using ExpenseTracker_Api.Interface;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.CustomException;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseTracker_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<AccountDto> _validator;

        public AccountController(IAccountService accountService, IValidator<AccountDto> validator)
        {
            _accountService = accountService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchTerm = "")
        {
            ApiResponses apiResponses = await _accountService.RetrieveAllAccounts(pageNumber, pageSize, searchTerm);

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
        public async Task<IActionResult> Add(AccountDto accountDto)
        {
            ApiResponses apiResponses = await _accountService.SaveAccount(accountDto);

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
        public async Task<IActionResult> Edit(int id, [FromBody] AccountDto accountDto)
        {
            ApiResponses apiResponses =  await _accountService.ModifyAccount(id, accountDto);
            
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
            ApiResponses apiResponses = await _accountService.RemoveAccount(id);

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
