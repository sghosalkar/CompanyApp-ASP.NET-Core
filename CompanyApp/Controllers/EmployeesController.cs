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
                TempData["errorMessage"] = "Incorrect Password";
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
            employee.EmployeeProject = employeeRepository.GetProjectsById(Id).ToList()
                .Select(p => new EmployeeProject { Project = p, ProjectId = p.Id, Employee = employee, EmployeeId = employee.Id }).ToList();
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
            await employeeRepository.SignOut();
            return RedirectToAction(nameof(Index));
        }

    }
}