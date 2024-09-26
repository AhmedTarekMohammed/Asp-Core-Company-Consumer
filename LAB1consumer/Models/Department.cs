using System.ComponentModel.DataAnnotations;

namespace LAB1consumer.Models
{
    public class Department
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Display(Name = "Employees Names")]
        public List<string> empNames { get; set; } = new List<string>();

        [Display(Name = "Projects ")]

        public List<string> projectsNames { get; set; } = new List<string>();
    }
}
