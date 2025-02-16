using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseAlertsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiseaseAlertsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/DiseaseAlerts
        [HttpGet]
        public async Task<IActionResult> GetDiseaseAlerts()
        {
            var alerts = await _unitOfWork.DiseaseAlerts.GetAllAsync();
            var alertDtos = alerts.Select(a => new DiseaseAlertDto
            {
                Id = a.Id,
                FarmId = a.FarmId,
                AlertDate = a.AlertDate,
                Message = a.Message
            });
            return Ok(alertDtos);
        }

        // GET: api/DiseaseAlerts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiseaseAlert(int id)
        {
            var alert = await _unitOfWork.DiseaseAlerts.GetByIdAsync(id);
            if (alert == null)
                return NotFound();

            var alertDto = new DiseaseAlertDto
            {
                Id = alert.Id,
                FarmId = alert.FarmId,
                AlertDate = alert.AlertDate,
                Message = alert.Message
            };
            return Ok(alertDto);
        }

        // POST: api/DiseaseAlerts
        [HttpPost]
        public async Task<IActionResult> CreateDiseaseAlert([FromBody] DiseaseAlertDto alertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var alert = new DiseaseAlert
            {
                FarmId = alertDto.FarmId,
                AlertDate = alertDto.AlertDate,
                Message = alertDto.Message
            };

            await _unitOfWork.DiseaseAlerts.AddAsync(alert);
            await _unitOfWork.CompleteAsync();
            alertDto.Id = alert.Id;

            return CreatedAtAction(nameof(GetDiseaseAlert), new { id = alert.Id }, alertDto);
        }

        // PUT: api/DiseaseAlerts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiseaseAlert(int id, [FromBody] DiseaseAlertDto alertDto)
        {
            if (id != alertDto.Id)
                return BadRequest();

            var alert = await _unitOfWork.DiseaseAlerts.GetByIdAsync(id);
            if (alert == null)
                return NotFound();

            alert.FarmId = alertDto.FarmId;
            alert.AlertDate = alertDto.AlertDate;
            alert.Message = alertDto.Message;

            _unitOfWork.DiseaseAlerts.Update(alert);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/DiseaseAlerts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseaseAlert(int id)
        {
            var alert = await _unitOfWork.DiseaseAlerts.GetByIdAsync(id);
            if (alert == null)
                return NotFound();

            _unitOfWork.DiseaseAlerts.Remove(alert);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
