using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalFarmingApi.Models.DTO;
using VerticalFarmingApi.Models.DTO__Data_Transfer_Objects_;
using VerticalFarmingApi.Models;
using VerticalFarmingApi.Repositories.IRepository;

namespace VerticalFarmingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropHealthReportsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CropHealthReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD Endpoints for CropHealthReport Entity

        // GET: api/CropHealthReports
        [HttpGet]
        public async Task<IActionResult> GetCropHealthReports()
        {
            var reports = await _unitOfWork.CropHealthReports.GetAllAsync();
            var reportDtos = reports.Select(r => new CropHealthReportDto
            {
                Id = r.Id,
                FarmId = r.FarmId,
                ReportDate = r.ReportDate,
                Summary = r.Summary
            });
            return Ok(reportDtos);
        }

        // GET: api/CropHealthReports/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCropHealthReport(int id)
        {
            var report = await _unitOfWork.CropHealthReports.GetByIdAsync(id);
            if (report == null)
                return NotFound();

            var reportDto = new CropHealthReportDto
            {
                Id = report.Id,
                FarmId = report.FarmId,
                ReportDate = report.ReportDate,
                Summary = report.Summary
            };
            return Ok(reportDto);
        }

        // POST: api/CropHealthReports
        [HttpPost]
        public async Task<IActionResult> CreateCropHealthReport([FromBody] CropHealthReportDto reportDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var report = new CropHealthReport
            {
                FarmId = reportDto.FarmId,
                ReportDate = reportDto.ReportDate,
                Summary = reportDto.Summary
            };

            await _unitOfWork.CropHealthReports.AddAsync(report);
            await _unitOfWork.CompleteAsync();
            reportDto.Id = report.Id;

            return CreatedAtAction(nameof(GetCropHealthReport), new { id = report.Id }, reportDto);
        }

        // PUT: api/CropHealthReports/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCropHealthReport(int id, [FromBody] CropHealthReportDto reportDto)
        {
            if (id != reportDto.Id)
                return BadRequest();

            var report = await _unitOfWork.CropHealthReports.GetByIdAsync(id);
            if (report == null)
                return NotFound();

            report.FarmId = reportDto.FarmId;
            report.ReportDate = reportDto.ReportDate;
            report.Summary = reportDto.Summary;

            _unitOfWork.CropHealthReports.Update(report);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/CropHealthReports/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCropHealthReport(int id)
        {
            var report = await _unitOfWork.CropHealthReports.GetByIdAsync(id);
            if (report == null)
                return NotFound();

            _unitOfWork.CropHealthReports.Remove(report);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        #endregion

        #region Aggregated Full Report Endpoints

        // GET: api/CropHealthReports/report?farmId={farmId}
        [HttpGet("report")]
        public async Task<IActionResult> GetFullReport([FromQuery] int? farmId)
        {
            List<Farm> farms;
            if (farmId.HasValue)
            {
                var farm = await _unitOfWork.Farms.GetByIdAsync(farmId.Value);
                if (farm == null)
                    return NotFound("Farm not found.");
                farms = new List<Farm> { farm };
            }
            else
            {
                farms = (await _unitOfWork.Farms.GetAllAsync()).ToList();
            }

            if (farms == null || farms.Count == 0)
            {
                return NotFound("No farms found.");
            }

            var fullReports = new List<FullCropHealthReportDto>();

            foreach (var farm in farms)
            {
                if (farm == null) continue;

                // الحصول على بيانات المزارع (المالك) من الخاصية navigation (farm.User)
                var farmer = farm.User;
                // إذا لم تكن محملة، يمكن استدعاء _unitOfWork.Users.GetByIdAsync(farm.UserId)

                // جلب النباتات المرتبطة بالمزرعة
                // (يفترض أن الكلاس Crop يحتوي على مفتاح أجنبي FarmId)
                var crops = await _unitOfWork.Crops.FindAsync(c => c.FarmId == farm.Id);

                // جلب تنبيهات الأمراض الخاصة بالمزرعة
                var diseaseAlerts = await _unitOfWork.DiseaseAlerts.FindAsync(da => da.FarmId == farm.Id);

                // جلب أجهزة الاستشعار المرتبطة بالمزرعة (من الخاصية navigation في Farm)
                var sensors = farm.Sensors;

                // جلب أحدث تقرير صحي للمزرعة إن وُجد
                var reports = await _unitOfWork.CropHealthReports.FindAsync(r => r.FarmId == farm.Id);
                CropHealthReportDto reportDto = null;
                var latestReport = reports.OrderByDescending(r => r.ReportDate).FirstOrDefault();
                if (latestReport != null)
                {
                    reportDto = new CropHealthReportDto
                    {
                        Id = latestReport.Id,
                        FarmId = latestReport.FarmId,
                        ReportDate = latestReport.ReportDate,
                        Summary = latestReport.Summary
                    };
                }

                var fullReport = new FullCropHealthReportDto
                {
                    FarmId = farm.Id,
                    FarmName = farm.Name,
                    Location = farm.Location,
                    Farmer = new UserDTO
                    {
                        UserName = farmer?.Username,
                        Name = farmer?.Name,
                        Email = farmer?.Email,
                        Phone = farmer?.Phone
                    },
                    Crops = crops.Select(c => new CropDto
                    {
                        Id = c.Id,
                        Type = c.Type,
                        PlantingDate = c.PlantingDate,
                        SensorId = c.SensorId
                    }),
                    DiseaseAlerts = diseaseAlerts.Select(da => new DiseaseAlertDto
                    {
                        Id = da.Id,
                        FarmId = da.FarmId,
                        AlertDate = da.AlertDate,
                        Message = da.Message
                    }),
                    Sensors = sensors?.Select(s => new SensorDto
                    {
                        Id = s.Id,
                        Type = s.Type,
                        Description = s.Description
                    }),
                    Report = reportDto
                };

                fullReports.Add(fullReport);
            }

            return Ok(fullReports);
        }

        // PUT: api/CropHealthReports/report/{id}
        [HttpPut("report/{id}")]
        public async Task<IActionResult> UpdateCropHealthReportRecord(int id, [FromBody] CropHealthReportDto reportDto)
        {
            if (id != reportDto.Id)
                return BadRequest();

            var report = await _unitOfWork.CropHealthReports.GetByIdAsync(id);
            if (report == null)
                return NotFound();

            report.ReportDate = reportDto.ReportDate;
            report.Summary = reportDto.Summary;
            report.FarmId = reportDto.FarmId;

            _unitOfWork.CropHealthReports.Update(report);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/CropHealthReports/report/{id}
        [HttpDelete("report/{id}")]
        public async Task<IActionResult> DeleteCropHealthReportRecord(int id)
        {
            var report = await _unitOfWork.CropHealthReports.GetByIdAsync(id);
            if (report == null)
                return NotFound();

            _unitOfWork.CropHealthReports.Remove(report);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        #endregion
    }
}

