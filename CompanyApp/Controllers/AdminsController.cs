using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApp.Models;
using CompanyApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminsController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EmployeeRepository employeeRepository;
        private readonly RoleRepository roleRepository;
        private readonly PointRepository pointRepository;

        public AdminsController(EmployeeRepository employeeRepository, RoleRepository roleRepository, PointRepository pointRepository)
        {
            this.employeeRepository = employeeRepository;
            this.roleRepository = roleRepository;
            this.pointRepository = pointRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Member", "Employees");
            }
            ViewBag.AdminId = Id;
            var model = new EmployeeSelectionViewModel
            {
                Employees = employeeRepository.GetAll().Select(x => new SelectEmployeeViewModel() { Id = x.Id, Name = x.Name }).ToList()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SendSelected([Bind("Employees, AllocationValue")] EmployeeSelectionViewModel model, int AdminId)
        {
            var selectedIds = model.Employees.Where(m => m.IsSelected).Select(m => m.Id);
            var points = employeeRepository.GetAll().Where(e => selectedIds.Contains(e.Id))
                .Select(e => 
                new Point
                {
                    Employee = e,
                    Value = model.AllocationValue,
                    IsAward = false,
                    Timestamp = DateTime.Now,
                    ReceivedFrom = employeeRepository.GetById(AdminId)
                });
            points.ToList().ForEach(p => pointRepository.Add(p));
            pointRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SendAll([Bind("Employees, AllocationValue")] EmployeeSelectionViewModel model, int AdminId)
        {
            var points = employeeRepository.GetAll().Select(e =>
                new Point
                {
                    Employee = e,
                    Value = model.AllocationValue,
                    IsAward = false,
                    Timestamp = DateTime.Now,
                    ReceivedFrom = employeeRepository.GetById(AdminId)
                });
            points.ToList().ForEach(p => pointRepository.Add(p));
            pointRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}