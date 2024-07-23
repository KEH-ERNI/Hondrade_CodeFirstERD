using Hondrade_CodeFirstERD.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hondrade_CodeFirstERD.DTOs
{
    public class CitizenDto
    {
        [Key]
        public int CitizenID { get; set; }

        [MaxLength(50)]
        public string FName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? MName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LName { get; set; } = string.Empty;

        [MaxLength(225)]
        public string Address { get; set; } = string.Empty;
        public DateTime Bday { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string UName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ImgName { get; set; } = string.Empty;

        public ICollection<ApplicationDto>? Applications { get; set; }

        public ICollection<ContactDto>? Contacts { get; set; }
    }

}
