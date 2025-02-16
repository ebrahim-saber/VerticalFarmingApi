namespace VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_
{
    public class SensorDataDto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public int SensorId { get; set; }
    }
}
