using Hondrade_CodeFirstERD.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hondrade_CodeFirstERD.DTOs
{
    public class EmployeeDto
    {
        [Key]
        public int EmpID { get; set; }

        [MaxLength(50)]
        public string FName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? MName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LName { get; set; } = string.Empty;

        public int DepID { get; set; }

        [MaxLength(50)]
        public string Position { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string UName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ImgName { get; set; } = string.Empty;
        public ICollection<ContactDto>? Contacts { get; set; }
    }
}
