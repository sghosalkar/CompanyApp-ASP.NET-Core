using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.DTO;
using CompanyApp.Models;
using CompanyApp.Repositories;

namespace CompanyApp.Controllers
{
    public class ProjectsController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EmployeeRepository employeeRepository;
        private readonly ProjectRepository projectRepository;

        public ProjectsController(ProjectRepository projectRepository, EmployeeRepository employeeRepository)
        {
            this.projectRepository = projectRepository;
            this.employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            List<Project> Projects = projectRepository.GetAll().ToList();
            return View(Projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                projectRepository.Add(project);
                projectRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            log.Debug("Invalid state");
            return View(project);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Project project = projectRepository.GetById(Id);
            IEnumerable<Employee> projectEmployees = project.ProjectEmployee.Select(e => e.Employee);
            IEnumerable<Employee> otherEmployees = employeeRepository.GetAll().Except(projectEmployees);
            ViewBag.otherEmployees = otherEmployees;
            return View(project);
        }

        [HttpPost]
        public IActionResult AddEmployeeToProject(int employeeId, int Id)
        {
            EmployeeProjectDto employeeProjectDto = new EmployeeProjectDto
            {
                EmployeeId = employeeId,
                ProjectId = Id
            };
            projectRepository.AddEmployeeToProject(employeeProjectDto);
            projectRepository.Save();
            return RedirectToAction(nameof(Details));
        }

        [HttpPost]
        public IActionResult RemoveEmployeeFromProject(int employeeId, int Id)
        {
            EmployeeProjectDto employeeProjectDto = new EmployeeProjectDto
            {
                EmployeeId = employeeId,
                ProjectId = Id
            };
            projectRepository.RemoveEmployeeFromProject(employeeProjectDto);
            projectRepository.Save();
            return RedirectToAction(nameof(Details));
        }

    }
}