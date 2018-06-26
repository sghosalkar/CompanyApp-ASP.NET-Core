using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    
    public class Department
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }

        public Department()
        {
            Employee = new HashSet<Employee>();
        }
    }
}
