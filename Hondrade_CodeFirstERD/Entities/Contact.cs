using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hondrade_CodeFirstERD.Entities
{
    public class Contact
    {

        [Key]
        public int ContactID { get; set; }

        public int EmpID { get; set; }
        [ForeignKey("EmpID")]
        public Employee Employee { get; set; }

        public int CitizenID { get; set; }
        [ForeignKey("CitizenID")]
        public Citizen Citizen { get; set; }

        [Required]
        public DateTime ContactDate { get; set; }

        [Required, MaxLength(50)]
        public string ContactMethod { get; set; } = string.Empty;

    }
}
