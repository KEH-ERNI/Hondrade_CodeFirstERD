
using Hondrade_CodeFirstERD.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hondrade_CodeFirstERD.DTOs
{
    public class ServiceDto
    {
        [Key]
        public int? ServiceID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(225)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ImgName { get; set; } = string.Empty;

        public Boolean IsActive { get; set; } = true;

        public int DepID { get; set; }

        public ICollection<ApplicationDto>? Applications { get; set; }

    }
}
