using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using CompanyApp.Data;
using CompanyApp.Models;
using CompanyApp.Repositories;

namespace CompanyApp.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EmployeeRepository employeeRepository;
        private readonly DepartmentRepository departmentRepository;
        private readonly RoleRepository roleRepository;
        private readonly PointRepository pointRepository;

        public EmployeesController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepository, RoleRepository roleRepository, PointRepository pointRepository)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
            this.roleRepository = roleRepository;
            this.pointRepository = pointRepository;
        }

        public IActionResult Index()
        {
            return View();

        }

        public async Task<IActionResult> SignIn([Bind("Id, Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var result = employeeRepository.GetById(user.Id);
                log.Debug(result);
                if (result == null)
                {
                    TempData["errorMessage"] = "User ID doesn't exist";
                    return RedirectToAction(nameof(Index));
                }
                if (result.Password.Equals(Security.HashSHA1(user.Password)))
                {
                    await employeeRepository.SignIn(result);
                    return RedirectToAction(nameof(Member), new { result.Id }); 
                }
                log.Debug("Password didn't match");
                TempData["errorMessage"] = "Incorrect Password";
            }
            else
            {
                log.Error("Invalid Model state.");
            }
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Member(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var employee = employeeRepository.GetById(Id);
            return View(employee);
        }

        public IActionResult List()
        {
            List<Employee> employees = employeeRepository.GetAll().ToList();
            return View(employees);

        }

        public IActionResult Create()
        {
            var departments = departmentRepository.GetAll().Select(x => new { x.Id, x.Name });
            List<SelectListItem> departmentItems = departments.Select(x => new SelectListItem() { Text = x.Id.ToString(), Value = x.Name }).ToList();
            ViewBag.departments = departmentItems;
            var roles = roleRepository.GetAll().Select(x => x.Name);
            List<SelectListItem> roleItems = roles.Select(x => new SelectListItem() { Text = x, Value = x }).ToList();
            ViewBag.roles = roleItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Department, Role, Password, ConfirmPassword")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                Department department = departmentRepository.GetById(employee.Department.Id);
                employee.Department = department;
                await employeeRepository.Add(employee);
                employeeRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            log.Debug("Invalid state");
            return View(employee);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Employee employee = employeeRepository.GetById(Id);
            log.Debug(employeeRepository.GetProjectsById(Id).ToList().Count());
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Employee employee = employeeRepository.GetById(Id);
            await employeeRepository.Remove(employee);
            employeeRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout()
        {
            log.Debug("Logout Action 1");
            await employeeRepository.SignOut();
            log.Debug("Logout Action 1");
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Admin = Id;
            var model = new EmployeeSelectionViewModel
            {
                Employees = employeeRepository.GetAll().Select(x => new SelectEmployeeViewModel() { Id = x.Id, Name = x.Name }).ToList()
            };
            return View(model);
            //return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SendSelected([Bind("Employees, AllocationValue")] EmployeeSelectionViewModel model, int Admin)
        {
            //log.Debug(ModelState.IsValid.ToString());
            var selectedIds = model.Employees.Where(m => m.IsSelected).Select(m => m.Id);
            log.Debug(selectedIds.ElementAt(0));
            var points =
                employeeRepository.GetAll()
                .Where(e => selectedIds.Contains(e.Id));
                //.Select(e => 
                //new Point
                //{
                //    Employee = e, Value = model.AllocationValue,
                //    IsAward = false, Timestamp = DateTime.Now,
                //    ReceivedFrom = employeeRepository.GetById(Admin)});
            //points.ToList().ForEach(p => pointRepository.Add(p));
            log.Debug(points.Count());
            //pointRepository.Save();
            log.Debug("Done");
            return RedirectToAction(nameof(Admin));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SendAll(EmployeeSelectionViewModel model)
        {
            return RedirectToAction(nameof(Admin));
        }
    }
}