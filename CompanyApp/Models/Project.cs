using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApp.Data;

namespace CompanyApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeProject> ProjectEmployee { get; set; }

        public Project()
        {
            ProjectEmployee = new HashSet<EmployeeProject>();
        }
    }
}
