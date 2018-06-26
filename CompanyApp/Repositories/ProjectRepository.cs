using CompanyApp.Data;
using CompanyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CompanyApp.DTO;

namespace CompanyApp.Repositories
{
    public class ProjectRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly CompanyDbContext context;

        public ProjectRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return context.Project.ToList();
        }

        public Project GetById(int? Id)
        {
            return context.Project.Include(p => p.ProjectEmployee).Single(p => p.Id == Id);
        }

        public void Add(Project Project)
        {
            context.Project.Add(Project);
        }

        public void Remove(Project Project)
        {
            context.Project.Remove(Project);
        }

        public void AddEmployeeToProject(EmployeeProjectDto dto)
        {
            log.Debug(GetById(dto.ProjectId).Name);
            context.Set<EmployeeProject>()
                .Add(new EmployeeProject {
                    EmployeeId = dto.EmployeeId,
                    Employee = context.Employee.Single(e => e.Id == dto.EmployeeId),
                    ProjectId = dto.ProjectId,
                    Project = GetById(dto.ProjectId)
                });
        }

        public void RemoveEmployeeFromProject(EmployeeProjectDto dto)
        {
            context.Set<EmployeeProject>()
                .Remove(new EmployeeProject
                {
                    EmployeeId = dto.EmployeeId,
                    ProjectId = dto.ProjectId
                });
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
