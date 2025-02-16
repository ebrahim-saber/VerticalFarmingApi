using VerticalFarmingApi.Models;

namespace VerticalFarmingApi.Repositories.IRepository
{
    public interface IUnitOfWork
    {
        IRepository<AIAnalysisResult> AIAnalysisResults { get; }
        IRepository<DiseaseAlert> DiseaseAlerts { get; }
        IRepository<Farm> Farms { get; }
        IRepository<Sensor> Sensors { get; }
        IRepository<SensorData> SensorDatas { get; }
        IRepository<CropHealthReport> CropHealthReports { get; }
        IRepository<Crop> Crops { get; }
        IRepository<SensorImage> SensorImages { get; }
        Task<int> CompleteAsync();
    }
}
