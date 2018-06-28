using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Repositories;
using CompanyApp.Data;

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

        [HttpGet]
        public string GetAll()
        {
            List<IdentityRole> roles = roleRepository.GetAll().ToList();
            string Json = Serialization.Serialize(roles);
            return Json;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> Create([Bind("Name")][FromBody] IdentityRole role)
        {
            return await roleRepository.AddAsync(role);
        }

        [HttpDelete]
        public async Task<bool> Delete(string Id)
        {
            return await roleRepository.RemoveAsync(Id);
            
        }
    }
}