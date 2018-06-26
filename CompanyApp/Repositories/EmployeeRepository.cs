using CompanyApp.Data;
using CompanyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// DAL - Data Access Layer
namespace CompanyApp.Repositories
{
    public class EmployeeRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly CompanyDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public EmployeeRepository(CompanyDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IEnumerable<Employee> GetAll()
        {
            return context.Employee.ToList();
        }

        public Employee GetById(int? Id)
        {
            return context.Employee.Include(e => e.EmployeeProject).Include(e => e.Department).FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Employee> GetByDepartmentId(int? Id)
        {
            return context.Employee.Where(e => e.Department == context.Department.FirstOrDefault(d => d.Id == Id));
        }

        public IEnumerable<Project> GetProjectsById(int? Id)
        {
            return context.Employee.Where(e => e.Id == Id).SelectMany(p => p.EmployeeProject).Select(e => e.Project);
        }

        public async Task Add(Employee employee)
        {
            employee.Password = Security.HashSHA1(employee.Password);
            var user = new IdentityUser { UserName = employee.Name };
            var userCreation = await userManager.CreateAsync(user, employee.Password);
            if (!userCreation.Succeeded)
            {
                Errors(userCreation);
                return;
            }
            log.Debug("User Created");
            var addRole = await userManager.AddToRoleAsync(user, employee.Role);
            if (!addRole.Succeeded)
            {
                Errors(addRole);
                return;
            }
            log.Debug("User Added to Role");
            context.Employee.Add(employee);
        }

        public async Task Remove(Employee employee)
        {
            IdentityUser user = await userManager.FindByNameAsync(employee.Name);
            var removeRole = await userManager.RemoveFromRoleAsync(user, employee.Role);
            if (!removeRole.Succeeded)
            {
                Errors(removeRole);
                return;
            }
            var userDeletion = await userManager.DeleteAsync(user);
            if (!userDeletion.Succeeded)
            {
                Errors(userDeletion);
                return;
            }
            
            context.Employee.Remove(employee);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                log.Debug(error.Code + ": " + error.Description);
            }
        }

        public async Task<IdentityUser> GetIdentityUserByName(string name)
        {
            return await userManager.FindByNameAsync(name);
        }

        public async Task SignIn(Employee employee)
        {
            IdentityUser user = await GetIdentityUserByName(employee.Name);
            await signInManager.SignInAsync(user, isPersistent: false);
            log.Debug("Signed In");
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
            log.Debug("Signed out");
        }
    }
}
