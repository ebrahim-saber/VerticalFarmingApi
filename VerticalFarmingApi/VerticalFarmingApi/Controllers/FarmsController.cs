using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FarmsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Farms
        [HttpGet]
        public async Task<IActionResult> GetFarms()
        {
            var farms = await _unitOfWork.Farms.GetAllAsync();
            var farmDtos = farms.Select(f => new FarmDto
            {
                Id = f.Id,
                Name = f.Name,
                Location = f.Location,
                UserId = f.UserId
            });
            return Ok(farmDtos);
        }

        // GET: api/Farms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFarm(int id)
        {
            var farm = await _unitOfWork.Farms.GetByIdAsync(id);
            if (farm == null)
                return NotFound();

            var farmDto = new FarmDto
            {
                Id = farm.Id,
                Name = farm.Name,
                Location = farm.Location,
                UserId = farm.UserId
            };
            return Ok(farmDto);
        }

        // POST: api/Farms
        [HttpPost]
        public async Task<IActionResult> CreateFarm([FromBody] FarmDto farmDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var farm = new Farm
            {
                Name = farmDto.Name,
                Location = farmDto.Location,
                UserId = farmDto.UserId
            };

            await _unitOfWork.Farms.AddAsync(farm);
            await _unitOfWork.CompleteAsync();

            farmDto.Id = farm.Id;
            return CreatedAtAction(nameof(GetFarm), new { id = farm.Id }, farmDto);
        }

        // PUT: api/Farms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFarm(int id, [FromBody] FarmDto farmDto)
        {
            if (id != farmDto.Id)
                return BadRequest();

            var farm = await _unitOfWork.Farms.GetByIdAsync(id);
            if (farm == null)
                return NotFound();

            farm.Name = farmDto.Name;
            farm.Location = farmDto.Location;
            farm.UserId = farmDto.UserId;

            _unitOfWork.Farms.Update(farm);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Farms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarm(int id)
        {
            var farm = await _unitOfWork.Farms.GetByIdAsync(id);
            if (farm == null)
                return NotFound();

            _unitOfWork.Farms.Remove(farm);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
