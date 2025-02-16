using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VerticalFarmingApi.Models
{
    public class AIAnalysisResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AnalysisDate { get; set; }

        [Required(ErrorMessage = "Health Status is required.")]
        [StringLength(50, ErrorMessage = "Health Status cannot exceed 50 characters.")]
        public string HealthStatus { get; set; } // مثل: Healthy, At Risk, Diseased


        [ForeignKey("Crop")]
        public int CropId { get; set; }
        public Crop Crop { get; set; }
    }
}

