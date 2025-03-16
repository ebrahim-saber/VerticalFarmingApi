using static System.Runtime.InteropServices.JavaScript.JSType;
using VerticalFarmingApi.Models.DTO;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Models;
using AutoMapper;

namespace VerticalFarmingApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Farm, FarmDto>().ReverseMap();
            CreateMap<Crop, CropDto>().ReverseMap();
            CreateMap<Sensor, SensorDto>().ReverseMap();
            CreateMap<SensorData, SensorDataDto>().ReverseMap();
            CreateMap<SensorImage, SensorImageDto>().ReverseMap();
            CreateMap<CropHealthReport, CropHealthReportDto>().ReverseMap();
            CreateMap<DiseaseAlert, DiseaseAlertDto>().ReverseMap();
            CreateMap<AIAnalysisResult, AIAnalysisResultDto>().ReverseMap();
        }
    }
}
