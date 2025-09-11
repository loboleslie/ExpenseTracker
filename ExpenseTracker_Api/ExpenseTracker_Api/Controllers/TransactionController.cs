using ExpenseTracker_Api.DTO;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> Index([FromQuery] TransactionSearchDto  searchDto)
        {
            ApiResponses apiResponses = await _transactionService.RetrieveAllTransaction(searchDto);

            if (apiResponses.StatusCode == 200)
            {
                return Ok(apiResponses);    
            }
            else
            {
                return BadRequest(apiResponses.Errors);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(TransactionDto transactionDto)
        {
            ApiResponses apiResponses = await _transactionService.SaveTransaction(transactionDto);

            if (apiResponses.StatusCode == 200)
            {
                return Ok(apiResponses);
            }
            else
            {
                return BadRequest(apiResponses.Errors);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TransactionDto transactionDto)
        {
            ApiResponses apiResponses = await _transactionService.ModifyTransaction(id, transactionDto);

            if (apiResponses.StatusCode == 200)
            {
                return Ok(apiResponses);
            }
            else
            {
                return BadRequest(apiResponses.Errors);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            ApiResponses apiResponses = await _transactionService.RemoveTransaction(id);

            if (apiResponses.StatusCode == 200)
            {
                return Ok(apiResponses);
            }
            else
            {
                return BadRequest(apiResponses.Errors);
            }
        }

    }
}
