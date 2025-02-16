using VerticalFarmingApi.Models;
using VerticalFarmingApi.Repositories.IRepository;
using VerticalFarmingApi.Services.IServices;

namespace VerticalFarmingApi.Services
{
    public class SensorImageCaptureService : IHostedService, IDisposable, ISensorImageCaptureService
    {
        private Timer _timer;
        private int _captureIntervalMinutes = 60; // الفترة الافتراضية: 60 دقيقة
        private readonly IWebHostEnvironment _env;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SensorImageCaptureService> _logger;

        public SensorImageCaptureService(IServiceScopeFactory scopeFactory, IWebHostEnvironment env, ILogger<SensorImageCaptureService> logger)
        {
            _scopeFactory = scopeFactory;
            _env = env;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SensorImageCaptureService starting.");
            _timer = new Timer(CaptureImageCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(_captureIntervalMinutes));
            return Task.CompletedTask;
        }

        private async void CaptureImageCallback(object state)
        {
            try
            {
                // إنشاء نطاق جديد للوصول إلى الخدمات Scoped مثل IUnitOfWork
                using (var scope = _scopeFactory.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    // تحديد مسار مجلد التخزين داخل wwwroot/uploads
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    // إنشاء اسم ملف فريد مع امتداد .jpg
                    var fileName = Guid.NewGuid().ToString() + ".jpg";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // هنا يتم التقاط الصورة من الكاميرا.
                    // في هذا المثال سنستخدم صورة وهمية (dummy.jpg) كمثال.
                    var dummyImagePath = Path.Combine(_env.WebRootPath, "dummy.jpg");
                    if (!File.Exists(dummyImagePath))
                    {
                        _logger.LogWarning("Dummy image not found. Skipping capture.");
                        return;
                    }
                    File.Copy(dummyImagePath, filePath);

                    // استرجاع جميع الحساسات من النوع esp32cam
                    var sensors = await unitOfWork.Sensors.FindAsync(s => s.Type.ToLower().Contains("esp32cam"));
                    foreach (var sensor in sensors)
                    {
                        var sensorImage = new SensorImage
                        {
                            SensorId = sensor.Id,
                            FileName = fileName,
                            FilePath = Path.Combine("uploads", fileName)
                        };
                        await unitOfWork.SensorImages.AddAsync(sensorImage);
                    }
                    await unitOfWork.CompleteAsync();

                    _logger.LogInformation("Sensor images captured and stored successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error capturing sensor images.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SensorImageCaptureService stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void UpdateCaptureInterval(int minutes)
        {
            _captureIntervalMinutes = minutes;
            _timer?.Change(TimeSpan.Zero, TimeSpan.FromMinutes(_captureIntervalMinutes));
            _logger.LogInformation("Capture interval updated to {0} minutes.", minutes);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

