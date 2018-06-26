using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\d]{4,}$", ErrorMessage = "The Password must be of atleast 4 characters.")]
        public string Password { get; set; }
    }

    public class Employee : User
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords doesnot match")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
        
        public virtual Department Department { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProject { get; set; }
        public virtual ICollection<Point> Point { get; set; }

        public Employee()
        {
            EmployeeProject = new HashSet<EmployeeProject>();
            Point = new HashSet<Point>();
        }
    }

    
    
}
