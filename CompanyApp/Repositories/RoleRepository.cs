using Microsoft.AspNetCore.Identity;
using CompanyApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Repositories
{
    public class RoleRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IEnumerable<IdentityRole> GetAll()
        {
            log.Debug(roleManager.Roles.Count());
            return roleManager.Roles.ToList();
        }

        public async Task<IdentityRole> GetByNameAsync(string name)
        {
            IdentityRole role = await roleManager.FindByNameAsync(name);
            return role;
        }

        public async Task<IdentityRole> GetByIdAsync(string Id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(Id);
            return role;
        }

        public async Task AddAsync(IdentityRole role)
        {
            var roleExist = await roleManager.RoleExistsAsync(role.Name);
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(role);
                log.Debug("Role created");
            }
        }

        public async Task RemoveAsync(string Id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(Id);
            var res = await userManager.GetUsersInRoleAsync(role.Name);
            if(res.Count > 0)
            {
                return;
            }
            var result = await roleManager.DeleteAsync(role);
        }
        
    }
}
