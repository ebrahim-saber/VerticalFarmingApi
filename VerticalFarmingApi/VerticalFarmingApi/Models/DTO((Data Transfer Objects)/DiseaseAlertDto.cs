namespace VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_
{
    public class DiseaseAlertDto
    {
        public int Id { get; set; }
        public int FarmId { get; set; }
        public DateTime AlertDate { get; set; }
        public string Message { get; set; }
    }
}
