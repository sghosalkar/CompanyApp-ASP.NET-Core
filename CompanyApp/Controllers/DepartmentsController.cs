using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Models;
using CompanyApp.Repositories;
using CompanyApp.Data;
using Newtonsoft.Json;

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

        public string GetAll()
        {
            List<Department> Departments = departmentRepository.GetAll().ToList();
            string json = Serialization.Serialize(Departments);
            log.Debug(json);
            return json;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool Create([Bind("Name")][FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepository.Add(department);
                departmentRepository.Save();
                return true;
            }
            return false;
        }

        [HttpGet]
        public string Details(int? Id)
        {
            if (Id != null)
            {
                Department department = departmentRepository.GetById(Id);
                department.Employee = employeeRepository.GetByDepartmentId(Id).ToList();
                string json = Serialization.Serialize(department);
                return json;
            }
            return null;
        }

        [HttpDelete]
        public void Delete(int? Id)
        {
            if (Id != null)
            {
                Department department = departmentRepository.GetById(Id);
                departmentRepository.Remove(department);
                departmentRepository.Save();
            }
        }
    }
}