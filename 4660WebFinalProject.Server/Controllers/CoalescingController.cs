using _4660FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _4660WebFinalProject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoalescingController : ControllerBase
    {
        private readonly TemporalCoalescingService _coalescingService;
        private readonly JSONToMySQLService _jsonService;

        public CoalescingController(TemporalCoalescingService coalescingService, JSONToMySQLService jsonService)
        {
            _coalescingService = coalescingService;
            _jsonService = jsonService;
        }

        [HttpGet("sql-coalesce-small")]
        public IActionResult CoalesceUsingSQLSmall()
        {
            var result = _coalescingService.CoalesceUsingSQLSmall();
            return Ok(result);
        }

        [HttpGet("single-scan-coalesce-small")]
        public IActionResult CoalesceUsingSingleScanSmall()
        {
            var result = _coalescingService.CoalesceUsingSingleScanSmall();
            return Ok(result);
        }

        [HttpGet("sql-coalesce-medium")]
        public IActionResult CoalesceUsingSQLMedium()
        {
            var result = _coalescingService.CoalesceUsingSQLMedium();
            return Ok(result);
        }

        [HttpGet("single-scan-coalesce-medium")]
        public IActionResult CoalesceUsingSingleScanMedium()
        {
            var result = _coalescingService.CoalesceUsingSingleScanMedium();
            return Ok(result);
        }

        [HttpGet("sql-coalesce-large")]
        public IActionResult CoalesceUsingSQLLarge()
        {
            var result = _coalescingService.CoalesceUsingSQLLarge();
            return Ok(result);
        }

        [HttpGet("single-scan-coalesce-large")]
        public IActionResult CoalesceUsingSingleScanLarge()
        {
            var result = _coalescingService.CoalesceUsingSingleScanLarge();
            return Ok(result);
        }

        [HttpPost("upload-small-json-to-sql")]
        public IActionResult UploadSmallJSONToSQL()
        {
            try
            {
                _jsonService.InsertSmallJSONDataToSQL();
                return Ok("Data uploaded successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error uploading data: {ex.Message}");
            }
        }

        [HttpPost("upload-medium-json-to-sql")]
        public IActionResult UploadMediumJSONToSQL()
        {
            try
            {
                _jsonService.InsertMediumJSONDataToSQL();
                return Ok("Data uploaded successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error uploading data: {ex.Message}");
            }
        }

        [HttpPost("upload-large-json-to-sql")]
        public IActionResult UploadLargeJSONToSQL()
        {
            try
            {
                _jsonService.InsertLargeJSONDataToSQL();
                return Ok("Data uploaded successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error uploading data: {ex.Message}");
            }
        }
    }
}
