using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker_Api.Services;
using System.IO;
using ExpenseTracker_Api.Interface.Services;

namespace ExpenseTracker_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkProcessTransactionController : ControllerBase
    {
        private readonly IBulkProcessService _bulkProcessService;

        public BulkProcessTransactionController(IBulkProcessService bulkProcessService)
        {
            _bulkProcessService = bulkProcessService;
        }

        [HttpPost("csv")]
        public async Task<IActionResult> UploadTransactionCsv(IFormFile file, [FromQuery] int accountId = 1)
        {
            if (file == null || file.Length == 0)
                return BadRequest("CSV file is required.");

            // Optional: Validate file extension
            if (!Path.GetExtension(file.FileName).Equals(".csv", System.StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only .csv files are allowed.");

            string csvContent;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                csvContent = await reader.ReadToEndAsync();
            }

            try
            {
                var result = _bulkProcessService.ProcessCsv(csvContent, accountId);
                return Ok(new
                {
                    Message = "CSV processed.",
                    SuccessCount = result.SuccessCount,
                    FailureCount = result.FailureCount,
                    Errors = result.ErrorMessages
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing CSV: {ex.Message}");
            }
        }
    }
}
