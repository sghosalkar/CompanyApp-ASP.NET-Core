using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Models
{
    public class Point
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsAward { get; set; }

        public virtual Employee ReceivedFrom { get; set; }
        public virtual Employee Employee { get; set; }
    }

    public class SelectEmployeeViewModel
    {
        public bool IsSelected { get; set; } = false;
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class EmployeeSelectionViewModel
    {
        public IEnumerable<SelectEmployeeViewModel> Employees { get; set; }

        public int AllocationValue { get; set; }

        //public int 
        //public Point Point { get; set; }

        public EmployeeSelectionViewModel()
        {
            Employees = new List<SelectEmployeeViewModel>();
        }
        //public IEnumerable<int> getSelectedIds()
        //{
        //    // Return an Enumerable containing the Id's of the selected people:
        //    return (from p in this.People where p.Selected select p.Id).ToList();
        //}
    }
}
