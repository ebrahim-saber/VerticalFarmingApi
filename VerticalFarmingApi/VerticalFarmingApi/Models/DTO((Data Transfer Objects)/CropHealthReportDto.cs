namespace VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_
{
    public class CropHealthReportDto
    {
        public int Id { get; set; }
        public int FarmId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Summary { get; set; }
    }
}
