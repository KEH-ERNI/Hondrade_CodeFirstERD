using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hondrade_CodeFirstERD.Entities
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }

        public int CitizenID { get; set; }
        [ForeignKey("CitizenID")]
        public Citizen Citizen { get; set; }

        [Required]
        public DateTime SubmittedDate { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; } = "Pending";

        public int ServiceID { get; set; }
        [ForeignKey("ServiceID")]
        public Service Service { get; set; }

    }
}
