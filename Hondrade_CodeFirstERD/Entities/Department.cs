using System.ComponentModel.DataAnnotations;

namespace Hondrade_CodeFirstERD.Entities
{
    public class Department
    {
        [Key]
        public int DepID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(225)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime EstDate { get; set; }

        public ICollection<Service> Services { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
