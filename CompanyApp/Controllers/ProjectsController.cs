using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.DTO;
using CompanyApp.Models;
using CompanyApp.Repositories;
using CompanyApp.Data;

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

        [HttpGet]
        public string GetAll()
        {
            List<Project> Projects = projectRepository.GetAll().ToList();
            string json = Serialization.Serialize(Projects);
            return json;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool Create([Bind("Name")][FromBody] Project project)
        {
            if (ModelState.IsValid)
            {
                projectRepository.Add(project);
                projectRepository.Save();
                return true;
            }
            log.Debug("Invalid state");
            return false;
        }

        [HttpGet]
        public string Details(int? Id)
        {
            if (Id != null)
            {
                Project project = projectRepository.GetById(Id);
                IEnumerable<Employee> projectEmployees = project.ProjectEmployee.Select(e => e.Employee);
                IEnumerable<Employee> otherEmployees = employeeRepository.GetAll().Except(projectEmployees);
                string json = Serialization.Serialize(new { project, projectEmployees, otherEmployees });
                return json;
            }
            return null;
        }

        [HttpPost]
        public void AddEmployeeToProject([FromBody]int employeeId,[FromBody] int Id)
        {
            EmployeeProjectDto employeeProjectDto = new EmployeeProjectDto
            {
                EmployeeId = employeeId,
                ProjectId = Id
            };
            projectRepository.AddEmployeeToProject(employeeProjectDto);
            projectRepository.Save();
        }

        [HttpPost]
        public void RemoveEmployeeFromProject([FromBody]int employeeId, [FromBody]int Id)
        {
            EmployeeProjectDto employeeProjectDto = new EmployeeProjectDto
            {
                EmployeeId = employeeId,
                ProjectId = Id
            };
            projectRepository.RemoveEmployeeFromProject(employeeProjectDto);
            projectRepository.Save();
        }

    }
}