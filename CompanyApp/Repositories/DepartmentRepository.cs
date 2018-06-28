using CompanyApp.Data;
using CompanyApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Repositories
{
    public class DepartmentRepository
    {
        private readonly CompanyDbContext context;

        public DepartmentRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Department> GetAll()
        {
            return context.Department.Include(d => d.Employee).ToList();
        }

        public Department GetById(int? Id)
        {
            return context.Department.FirstOrDefault(d => d.Id == Id);
        }

        public void Add(Department Department)
        {
            context.Department.Add(Department);
        }

        public void Remove(Department Department)
        {
            context.Department.Remove(Department);
        }

        public string Save()
        {
            try
            {
                context.SaveChanges();
                return "Success";
            }
            catch(Exception e)
            {
                return e.GetType().Name + e.Message;
            }
        }
    }
}
