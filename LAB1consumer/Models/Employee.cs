using System.ComponentModel.DataAnnotations;

namespace LAB1consumer.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public double salary { get; set; }

        [Display(Name=" Department")]
        public string deptName { get; set; }

        [Display(Name = " Projects")]
        public List<string> projectsNames { get; set; } = new List<string>();
    }
}
