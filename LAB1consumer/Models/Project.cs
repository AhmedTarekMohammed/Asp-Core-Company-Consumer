using System.ComponentModel.DataAnnotations;

namespace LAB1consumer.Models
{
    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }

        [Display (Name="Department")]
        public string deptName { get; set; }

        [Display(Name = "Employees")]
        public List<string> empNames { get; set; } = new List<string>();
    }
}
