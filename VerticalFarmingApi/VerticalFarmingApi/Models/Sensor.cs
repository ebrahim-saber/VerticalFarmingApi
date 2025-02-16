using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerticalFarmingApi.Models
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }  // مثل: حرارة، رطوبة، pH، NPK، إلخ

        public string Description { get; set; }

        // علاقة مع الصور الخاصة بهذا الاستشعار
        public virtual ICollection<SensorImage> SensorImages { get; set; }
    }
}
