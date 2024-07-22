using System.ComponentModel.DataAnnotations;

namespace Hondrade_CodeFirstERD.DTOs
{
    public class ApplicationDto
    {
        [Key]
        public int ApplicationID { get; set; }

        public DateTime SubmittedDate { get; set; }

        public string Status { get; set; } = "Pending";

        public int CitizenID { get; set; }

        public int ServiceID { get; set; }
    }
}
