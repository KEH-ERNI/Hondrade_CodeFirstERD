using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hondrade_CodeFirstERD.Entities
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(225)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ImgName { get; set; } = string.Empty;

        public int DepID {  get; set; }
        [ForeignKey("DepID")]
        public Department Department { get; set; }

        public Boolean IsActive { get; set; } = true;

        public ICollection<Application> Applications { get; set; }
    }
}
