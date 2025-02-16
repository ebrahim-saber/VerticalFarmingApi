using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CropsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Crops
        [HttpGet]
        public async Task<IActionResult> GetCrops()
        {
            var crops = await _unitOfWork.Crops.GetAllAsync();
            var cropDtos = crops.Select(c => new CropDto
            {
                Id = c.Id,
                Type = c.Type,
                PlantingDate = c.PlantingDate,
                SensorId = c.SensorId
            });
            return Ok(cropDtos);
        }

        // GET: api/Crops/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCrop(int id)
        {
            var crop = await _unitOfWork.Crops.GetByIdAsync(id);
            if (crop == null)
                return NotFound();

            var cropDto = new CropDto
            {
                Id = crop.Id,
                Type = crop.Type,
                PlantingDate = crop.PlantingDate,
                SensorId = crop.SensorId
            };
            return Ok(cropDto);
        }

        // POST: api/Crops
        [HttpPost]
        public async Task<IActionResult> CreateCrop([FromBody] CropDto cropDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // إذا تم تمرير SensorId وتريد التأكد من وجوده:
            if (cropDto.SensorId.HasValue)
            {
                var sensor = await _unitOfWork.Sensors.GetByIdAsync(cropDto.SensorId.Value);
                if (sensor == null)
                    return BadRequest("The specified SensorId does not exist.");
            }

            var crop = new Crop
            {
                Type = cropDto.Type,
                PlantingDate = cropDto.PlantingDate,
                FarmId = (int)cropDto.FarmId,
                SensorId = cropDto.SensorId 
            };

            await _unitOfWork.Crops.AddAsync(crop);
            await _unitOfWork.CompleteAsync();
            cropDto.Id = crop.Id;

            return CreatedAtAction(nameof(GetCrop), new { id = crop.Id }, cropDto);
        }


        // PUT: api/Crops/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCrop(int id, [FromBody] CropDto cropDto)
        {
            if (id != cropDto.Id)
                return BadRequest();

            var crop = await _unitOfWork.Crops.GetByIdAsync(id);
            if (crop == null)
                return NotFound();

            crop.Type = cropDto.Type;
            crop.PlantingDate = cropDto.PlantingDate;
            crop.SensorId = cropDto.SensorId;

            _unitOfWork.Crops.Update(crop);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Crops/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrop(int id)
        {
            var crop = await _unitOfWork.Crops.GetByIdAsync(id);
            if (crop == null)
                return NotFound();

            _unitOfWork.Crops.Remove(crop);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
