using VerticalFarmingApi.Models.DTO;

namespace VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_
{
    public class FullCropHealthReportDto
    {
        public int FarmId { get; set; }
        public string FarmName { get; set; }
        public string Location { get; set; }
        public UserDTO Farmer { get; set; }
        public IEnumerable<CropDto> Crops { get; set; }
        public IEnumerable<DiseaseAlertDto> DiseaseAlerts { get; set; }
        public IEnumerable<SensorDto> Sensors { get; set; }
        public CropHealthReportDto Report { get; set; }
    }
}
