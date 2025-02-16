using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VerticalFarmingApi.Models
{
    public class Farm
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Farm Name is required.")]
        [StringLength(150, ErrorMessage = "Farm Name cannot exceed 150 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        //[JsonIgnore]
        public User User { get; set; }

        //[JsonIgnore]
        public ICollection<Crop> Crops { get; set; }
  
        public ICollection<Sensor> Sensors { get; set; }
    }
}
