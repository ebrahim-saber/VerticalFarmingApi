using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIAnalysisResultsController : ControllerBase
    {
    private readonly IUnitOfWork _unitOfWork;

        public AIAnalysisResultsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/AIAnalysisResults
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _unitOfWork.AIAnalysisResults.GetAllAsync();
            var resultDtos = results.Select(r => new AIAnalysisResultDto
            {
                Id = r.Id,
                AnalysisDate = r.AnalysisDate,
                HealthStatus = r.HealthStatus,
                CropId = r.CropId
            });
            return Ok(resultDtos);
        }

        // GET: api/AIAnalysisResults/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _unitOfWork.AIAnalysisResults.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            var resultDto = new AIAnalysisResultDto
            {
                Id = result.Id,
                AnalysisDate = result.AnalysisDate,
                HealthStatus = result.HealthStatus,
                CropId = result.CropId
            };
            return Ok(resultDto);
        }

        // POST: api/AIAnalysisResults
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AIAnalysisResultDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = new AIAnalysisResult
            {
                AnalysisDate = dto.AnalysisDate,
                HealthStatus = dto.HealthStatus,
                CropId = dto.CropId
            };

            await _unitOfWork.AIAnalysisResults.AddAsync(result);
            await _unitOfWork.CompleteAsync();
            dto.Id = result.Id;
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, dto);
        }

        // PUT: api/AIAnalysisResults/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AIAnalysisResultDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Mismatched ID");

            var result = await _unitOfWork.AIAnalysisResults.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            result.AnalysisDate = dto.AnalysisDate;
            result.HealthStatus = dto.HealthStatus;
            result.CropId = dto.CropId;

            _unitOfWork.AIAnalysisResults.Update(result);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        // DELETE: api/AIAnalysisResults/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.AIAnalysisResults.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            _unitOfWork.AIAnalysisResults.Remove(result);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
