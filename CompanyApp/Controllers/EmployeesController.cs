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
using Microsoft.AspNetCore.Cors;

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

        [HttpPost]
        public async Task<string> SignIn([Bind("Id, Password")][FromBody] User user)
        {
            string response = null;
            if (ModelState.IsValid)
            {
                var result = employeeRepository.GetById(user.Id);
                if (result == null)
                {
                    response = "User ID doesn't exist"; ;
                }
                else if (result.Password.Equals(Security.HashSHA1(user.Password)))
                {
                    await employeeRepository.SignIn(result);
                    response = "success";
                }
                else
                {
                    response = "Incorrect Password";
                }
            }
            return Serialization.Serialize(response);
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

        [HttpGet]
        public string GetAll()
        {
            List<Employee> employees = employeeRepository.GetAll().ToList();
            string json = Serialization.Serialize(employees);
            return json;
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
        public async Task<bool> Create([Bind("Name, Department, Role, Password, ConfirmPassword")][FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                Department department = departmentRepository.GetById(employee.Department.Id);
                employee.Department = department;
                await employeeRepository.Add(employee);
                employeeRepository.Save();
                return true;
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
                foreach(var error in errors)
                {
                    log.Debug(error.Select(x => x.ErrorMessage) + " " + error.Select(x => x.Exception));
                }
                log.Debug(errors);
            }
            log.Debug("Invalid state");
            return false;
        }

        [HttpGet]
        public string Details(int? Id)
        {
            if (Id != null)
            {
                Employee employee = employeeRepository.GetById(Id);
                employee.EmployeeProject = employeeRepository.GetProjectsById(Id).ToList()
                    .Select(p => new EmployeeProject { Project = p, ProjectId = p.Id, Employee = employee, EmployeeId = employee.Id }).ToList();
                string json = Serialization.Serialize(employee);
                return json;
            }
            return null;
        }

        [HttpGet]
        public async Task<bool> Delete(int? Id)
        {
            if (Id != null)
            {
                Employee employee = employeeRepository.GetById(Id);
                await employeeRepository.Remove(employee);
                employeeRepository.Save();
                return true;
            }
            return false;
        }

        public async Task<IActionResult> Logout()
        {
            await employeeRepository.SignOut();
            return RedirectToAction(nameof(Index));
        }

        //public string VueInsert()
        //{
        //    log.Debug("VueInsert GET");
        //    return "done";
        //}

        public class Test
        {
            public string Name { get; set; }
        }
    }
    
}