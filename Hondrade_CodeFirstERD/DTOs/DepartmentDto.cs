using Hondrade_CodeFirstERD.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hondrade_CodeFirstERD.DTOs
{
    public class DepartmentDto
    {
        [Key]
        public int DepID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(225)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime EstDate { get; set; }

        public ICollection<ServiceDto>? Services { get; set; }

        public ICollection<EmployeeDto>? Employees { get; set; }
    }

}
