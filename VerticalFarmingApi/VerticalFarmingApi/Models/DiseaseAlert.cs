using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerticalFarmingApi.Models
{
    public class DiseaseAlert
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Farm")]
        public int FarmId { get; set; }

        [Required]
        public DateTime AlertDate { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; }

        public Farm Farm { get; set; }
    }
}
