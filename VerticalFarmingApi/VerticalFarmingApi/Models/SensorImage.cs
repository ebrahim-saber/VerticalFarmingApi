using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerticalFarmingApi.Models
{
    public class SensorImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SensorId { get; set; }

        [ForeignKey("SensorId")]
        public virtual Sensor Sensor { get; set; }

        [Required]
        public string FileName { get; set; } // اسم الملف المخزن على السيرفر

        [Required]
        public string FilePath { get; set; } // المسار النسبي (مثلاً: uploads\\filename.jpg)

    }
}
