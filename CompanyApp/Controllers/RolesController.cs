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

        public IActionResult Index()
        {
            List<IdentityRole> roles = roleRepository.GetAll().ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] IdentityRole role)
        {
            await roleRepository.AddAsync(role);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string Id)
        {
            await roleRepository.RemoveAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}