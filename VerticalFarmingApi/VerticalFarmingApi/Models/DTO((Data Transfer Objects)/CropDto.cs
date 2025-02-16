namespace VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_
{
    public class CropDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime PlantingDate { get; set; }
        public int? SensorId { get; set; }
        public int? FarmId { get; set; }

    }
}
