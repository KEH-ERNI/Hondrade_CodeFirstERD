using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hondrade_CodeFirstERD.Entities
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }

        [Required, MaxLength(50)]
        public string FName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? MName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LName { get; set; } = string.Empty;

        public int DepID { get; set; }
        [ForeignKey("DepID")]
        public Department Department { get; set; }

        [Required, MaxLength(50)]
        public string Position { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string UName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ImgName { get; set; } = string.Empty;

        public ICollection<Contact> Contacts { get; set; }
    }
}
