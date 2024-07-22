using Hondrade_CodeFirstERD.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hondrade_CodeFirstERD.DTOs
{
    public class ContactDto
    {
        [Key]
        public int ContactID { get; set; }

        public int? EmpID { get; set; }

        public int? CitizenID { get; set; }
    
        public DateTime ContactDate { get; set; }

        [MaxLength(50)]
        public string ContactMethod { get; set; } = string.Empty;
    }
}
