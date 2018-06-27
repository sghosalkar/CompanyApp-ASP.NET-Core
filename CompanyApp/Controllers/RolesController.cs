using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Repositories;

namespace CompanyApp.Controllers
{
    public class RolesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly RoleRepository roleRepository;
        

        public RolesController(RoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IEnumerable<IdentityRole> Index()
        {
            List<IdentityRole> roles = roleRepository.GetAll().ToList();
            return roles;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create([Bind("Name")][FromBody] IdentityRole role)
        {
            await roleRepository.AddAsync(role);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            await roleRepository.RemoveAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}