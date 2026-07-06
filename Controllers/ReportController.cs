using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObourLand.Entities;
using ObourLand.Models;
using ObourLand.Services;

namespace ObourLand.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : BaseController
    {
        private readonly ILogger<ReportController> _logger;
        private readonly ReportService _reportService;

        public ReportController(ILogger<ReportController> logger, ReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAsync([FromRoute] int userId)
        {
            _logger.LogInformation("Start GetReport method");
            return Ok(await _reportService.GetReports(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateReportDto report)
        {
            _logger.LogInformation("Start CreateReport method");

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var imagePaths = new List<string>();
            int count = 0;

            foreach (var image in report.Images)
            {
                count++;
                string fileName = $"{report.UserId}-{count}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{Path.GetExtension(image.FileName)}";
                string fullPath = Path.Combine(folderPath, fileName);
                _logger.LogInformation("verify the folder for upload the image");
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    _logger.LogInformation("Start uploading the Image");
                    await image.CopyToAsync(stream);
                    _logger.LogInformation("The Image Uploaded");
                }

                imagePaths.Add($"/Images/{fileName}");
            }

            var entity = new Report()
            {
                UserId = report.UserId,
                Date = report.Date,
                Time = report.Time,
                X = report.X,
                Y = report.Y,
                Comment = report.Comment,
                Image = string.Join(",", imagePaths)
            };
            _logger.LogInformation("Saving the report object");
            return Ok(await _reportService.CreateReport(entity));
        }
    }
}
