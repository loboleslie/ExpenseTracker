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
        public async Task<ApiResponses> Index()
        {
            ApiResponses apiResponses = await _accountService.RetrieveAllAccounts();

            return apiResponses;
        }

        [HttpPost]
        public async Task<ApiResponses> Add(AccountDto accountDto)
        {
            ApiResponses apiResponses = await _accountService.SaveAccount(accountDto);
            
            return apiResponses;
        }

        [HttpPut]
        public async Task<ApiResponses> Edit(AccountDto account)
        {
            ApiResponses apiResponses =  await _accountService.ModifyAccount(account);
            return apiResponses;
        }

        [HttpDelete]
        public async Task<ApiResponses> Delete(int id)
        {
            ApiResponses apiResponses = await _accountService.RemoveAccount(id);

            return apiResponses;
        }

    }
}
