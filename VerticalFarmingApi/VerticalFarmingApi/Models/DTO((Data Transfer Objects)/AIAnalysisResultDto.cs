namespace VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_
{
    public class AIAnalysisResultDto
    {
        public int Id { get; set; }
        public DateTime AnalysisDate { get; set; }
        public string HealthStatus { get; set; }
        public int CropId { get; set; }
    }
}
