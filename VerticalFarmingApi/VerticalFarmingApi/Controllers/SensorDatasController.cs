using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDatasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SensorDatasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/SensorDatas
        // يسترجع كل قراءات الحساسات
        [HttpGet]
        public async Task<IActionResult> GetSensorDatas()
        {
            var data = await _unitOfWork.SensorDatas.GetAllAsync();
            var dataDtos = data.Select(d => new SensorDataDto
            {
                Id = d.Id,
                Timestamp = d.Timestamp,
                Value = d.Value,
                SensorId = d.SensorId
            });
            return Ok(dataDtos);
        }

        // GET: api/SensorDatas/{id}
        // يسترجع قراءة حسّاس محددة بالـ id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorData(int id)
        {
            var data = await _unitOfWork.SensorDatas.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            var dataDto = new SensorDataDto
            {
                Id = data.Id,
                Timestamp = data.Timestamp,
                Value = data.Value,
                SensorId = data.SensorId
            };
            return Ok(dataDto);
        }

        // POST: api/SensorDatas
        // ينشئ قراءة حسّاس جديدة. تستخدم هذه النقطة لاستقبال البيانات المُرسلة من الحساسات مثل dht11، soil moisture، و mpk.
        [HttpPost]
        public async Task<IActionResult> CreateSensorData([FromBody] SensorDataDto dataDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = new SensorData
            {
                Timestamp = dataDto.Timestamp,
                Value = dataDto.Value,
                SensorId = dataDto.SensorId
            };

            await _unitOfWork.SensorDatas.AddAsync(data);
            await _unitOfWork.CompleteAsync();
            dataDto.Id = data.Id;

            return CreatedAtAction(nameof(GetSensorData), new { id = data.Id }, dataDto);
        }

        // PUT: api/SensorDatas/{id}
        // يسمح بتعديل قراءة حسّاس موجودة
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensorData(int id, [FromBody] SensorDataDto dataDto)
        {
            if (id != dataDto.Id)
                return BadRequest();

            var data = await _unitOfWork.SensorDatas.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            data.Timestamp = dataDto.Timestamp;
            data.Value = dataDto.Value;
            data.SensorId = dataDto.SensorId;

            _unitOfWork.SensorDatas.Update(data);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/SensorDatas/{id}
        // يحذف قراءة حسّاس معينة
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorData(int id)
        {
            var data = await _unitOfWork.SensorDatas.GetByIdAsync(id);
            if (data == null)
                return NotFound();

            _unitOfWork.SensorDatas.Remove(data);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
