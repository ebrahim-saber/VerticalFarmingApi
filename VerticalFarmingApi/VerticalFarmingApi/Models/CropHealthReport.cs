using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerticalFarmingApi.Models
{
    public class CropHealthReport
    {
        [Key]
        public int Id { get; set; }

        // المفتاح الخارجي للمزرعة
        [ForeignKey("Farm")]
        public int FarmId { get; set; }

        [Required]
        public DateTime ReportDate { get; set; }

        [Required(ErrorMessage = "Summary is required.")]
        [StringLength(1000, ErrorMessage = "Summary cannot exceed 1000 characters.")]
        public string Summary { get; set; }

        // العلاقة: التقرير يتبع مزرعة معينة
        public Farm Farm { get; set; }
    }
}
