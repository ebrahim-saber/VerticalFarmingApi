using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerticalFarmingApi.Models
{
    public class SensorData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public double Value { get; set; }

        public int SensorId { get; set; }

        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }
    }
}

