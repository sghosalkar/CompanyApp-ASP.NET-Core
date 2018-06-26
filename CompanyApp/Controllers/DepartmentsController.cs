using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Models;
using CompanyApp.Repositories;

namespace CompanyApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly DepartmentRepository departmentRepository;
        private readonly EmployeeRepository employeeRepository;

        public DepartmentsController(DepartmentRepository departmentRepository, EmployeeRepository employeeRepository)
        {
            this.departmentRepository = departmentRepository;
            this.employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            List<Department> Departments = departmentRepository.GetAll().ToList();
            return View(Departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepository.Add(department);
                departmentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Department department = departmentRepository.GetById(Id);
            department.Employee = employeeRepository.GetByDepartmentId(Id).ToList();

            return View(department);
        }
    }
}