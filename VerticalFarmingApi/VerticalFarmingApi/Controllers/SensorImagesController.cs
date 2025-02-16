using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Repositories.IRepository;
using VerticalFarmingApi.Services.IServices;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorImagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly ISensorImageCaptureService _captureService;

        public SensorImagesController(IUnitOfWork unitOfWork, IWebHostEnvironment env, ISensorImageCaptureService captureService)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _captureService = captureService;
        }

        // GET: api/SensorImages/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var sensorImage = await _unitOfWork.SensorImages.GetByIdAsync(id);
            if (sensorImage == null)
                return NotFound();

            var filePath = Path.Combine(_env.WebRootPath, sensorImage.FilePath);
            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found on server.");

            var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(imageBytes, "image/jpeg");
        }

        // DELETE: api/SensorImages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var sensorImage = await _unitOfWork.SensorImages.GetByIdAsync(id);
            if (sensorImage == null)
                return NotFound();

            var filePath = Path.Combine(_env.WebRootPath, sensorImage.FilePath);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _unitOfWork.SensorImages.Remove(sensorImage);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // PUT: api/SensorImages/updateInterval/{minutes}
        [HttpPut("updateInterval/{minutes}")]
        public IActionResult UpdateCaptureInterval(int minutes)
        {
            _captureService.UpdateCaptureInterval(minutes);
            return Ok($"Capture interval updated to {minutes} minutes.");
        }
    }
}

