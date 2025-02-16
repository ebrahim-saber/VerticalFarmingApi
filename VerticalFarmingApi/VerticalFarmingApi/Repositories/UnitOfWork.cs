using VerticalFarmingApi.Data;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<AIAnalysisResult> _aiAnalysisResults;
        private IRepository<DiseaseAlert> _diseaseAlerts;
        private IRepository<Farm> _farms;
        private IRepository<Sensor> _sensors;
        private IRepository<SensorData> _sensorDatas;
        private IRepository<CropHealthReport> _cropHealthReports;
        private IRepository<Crop> _crops;
        private IRepository<SensorImage> _sensorImages;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<AIAnalysisResult> AIAnalysisResults => _aiAnalysisResults ??= new Repository<AIAnalysisResult>(_context);
        public IRepository<DiseaseAlert> DiseaseAlerts => _diseaseAlerts ??= new Repository<DiseaseAlert>(_context);
        public IRepository<Farm> Farms => _farms ??= new Repository<Farm>(_context);
        public IRepository<Sensor> Sensors => _sensors ??= new Repository<Sensor>(_context);
        public IRepository<SensorData> SensorDatas => _sensorDatas ??= new Repository<SensorData>(_context);
        public IRepository<CropHealthReport> CropHealthReports => _cropHealthReports ??= new Repository<CropHealthReport>(_context);
        public IRepository<Crop> Crops => _crops ??= new Repository<Crop>(_context);
        public IRepository<SensorImage> SensorImages => _sensorImages ??= new Repository<SensorImage>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

