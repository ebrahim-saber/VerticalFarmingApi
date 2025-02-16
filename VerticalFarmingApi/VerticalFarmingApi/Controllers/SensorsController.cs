using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SensorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Sensors
        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            var sensors = await _unitOfWork.Sensors.GetAllAsync();
            var sensorDtos = sensors.Select(s => new SensorDto
            {
                Id = s.Id,
                Type = s.Type,
                Description = s.Description
            });
            return Ok(sensorDtos);
        }

        // GET: api/Sensors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensor(int id)
        {
            var sensor = await _unitOfWork.Sensors.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            var sensorDto = new SensorDto
            {
                Id = sensor.Id,
                Type = sensor.Type,
                Description = sensor.Description
            };
            return Ok(sensorDto);
        }

        // POST: api/Sensors
        [HttpPost]
        public async Task<IActionResult> CreateSensor([FromBody] SensorDto sensorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sensor = new Sensor
            {
                Type = sensorDto.Type,
                Description = sensorDto.Description
            };

            await _unitOfWork.Sensors.AddAsync(sensor);
            await _unitOfWork.CompleteAsync();
            sensorDto.Id = sensor.Id;

            return CreatedAtAction(nameof(GetSensor), new { id = sensor.Id }, sensorDto);
        }

        // PUT: api/Sensors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(int id, [FromBody] SensorDto sensorDto)
        {
            if (id != sensorDto.Id)
                return BadRequest();

            var sensor = await _unitOfWork.Sensors.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            sensor.Type = sensorDto.Type;
            sensor.Description = sensorDto.Description;

            _unitOfWork.Sensors.Update(sensor);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Sensors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _unitOfWork.Sensors.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            _unitOfWork.Sensors.Remove(sensor);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
