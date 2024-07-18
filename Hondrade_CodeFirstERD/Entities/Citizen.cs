using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Hondrade_CodeFirstERD.Entities
{
    public class Citizen
    {
        [Key]
        public int CitizenID { get; set; }

        [Required, MaxLength(50)]
        public string FName { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? MName { get; set;} = string.Empty;
        
        [Required, MaxLength(50)]
        public string LName { get; set; } = string.Empty;

        [Required, MaxLength(225)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime Bday { get; set; }

        [Required, MaxLength(50)]
        public string Phone{ get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string UName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ImgName { get; set; } = string.Empty;

        public ICollection<Application> Applications { get; set; }

        public ICollection<Contact> Contacts { get; set; }

    }
}
