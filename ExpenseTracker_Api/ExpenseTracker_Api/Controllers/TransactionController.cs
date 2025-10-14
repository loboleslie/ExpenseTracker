using ExpenseTracker_Api.DTO;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IValidator<TransactionDto> _validator;

        public TransactionController(ITransactionService transactionService, IValidator<TransactionDto> validator)
        {
            _transactionService = transactionService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index([FromQuery] TransactionSearchDto searchDto)
        {
            ApiResponses apiResponses = await _transactionService.RetrieveAllTransaction(searchDto);
            return apiResponses.StatusCode == 200 
                ? Ok(apiResponses) 
                : BadRequest(apiResponses);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] TransactionDto transactionDto)
        {
            var validationResult = await _validator.ValidateAsync(transactionDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponses 
                { 
                    StatusCode = 400, 
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList() 
                });
            }

            ApiResponses apiResponses = await _transactionService.SaveTransaction(transactionDto);
            if (apiResponses.StatusCode == StatusCodes.Status201Created && apiResponses.Result is int transactionId)
            {
                var resourceUri = Url.Action(nameof(Index), "Transaction", new { id = transactionId }, Request.Scheme);
                return Created(resourceUri!, apiResponses);
            }
            return BadRequest(apiResponses);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [FromBody] TransactionDto transactionDto)
        {
            var validationResult = await _validator.ValidateAsync(transactionDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponses 
                { 
                    StatusCode = 400, 
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList() 
                });
            }

            ApiResponses apiResponses = await _transactionService.ModifyTransaction(id, transactionDto);
            if (apiResponses.StatusCode == 404)
                return NotFound(apiResponses);
                
            return apiResponses.StatusCode == 200 
                ? Ok(apiResponses) 
                : BadRequest(apiResponses);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponses), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            ApiResponses apiResponses = await _transactionService.RemoveTransaction(id);
            if (apiResponses.StatusCode == 404)
                return NotFound(apiResponses);
                
            return apiResponses.StatusCode == 200 
                ? Ok(apiResponses) 
                : BadRequest(apiResponses);
        }
    }
}
