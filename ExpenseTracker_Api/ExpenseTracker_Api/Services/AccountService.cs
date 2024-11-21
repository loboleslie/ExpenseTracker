using AutoMapper;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.CustomException;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Principal;

namespace ExpenseTracker_Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private IValidator<AccountDto> _validator;
        private IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IValidator<AccountDto> validator, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResponses> ModifyAccount(AccountDto accountDto)
        {            
            
            ApiResponses apiResponses = new ApiResponses();

            try
            {

                ValidationResult result = await _validator.ValidateAsync(accountDto);

                if (!result.IsValid)
                {

                    foreach (var error in result.Errors)
                    {
                        apiResponses.Errors.Add(error.ErrorMessage);
                    }

                    throw new AccountValidationException();
                }
                else
                {
                    Account _account = _mapper.Map<Account>(accountDto);                    

                    _accountRepository.Update(_account);

                    apiResponses.Errors = null;
                    apiResponses.Result = _account;
                    apiResponses.StatusCode = 200;
                }

            }           
            catch (AccountValidationException)
            {
                apiResponses.StatusCode = 400;
            }
            catch (Exception ex)
            {
                apiResponses.Errors.Add("Internal Server Error");
                apiResponses.StatusCode = 500;
            }

            return apiResponses;
           
        }

        public async Task<ApiResponses> RemoveAccount(int accountId)
        {
            ApiResponses apiResponses = new ApiResponses();

            try
            {
                apiResponses.Errors = null;
                apiResponses.Result = _accountRepository.Delete(accountId);
                apiResponses.StatusCode = 200;
            }
            catch (Exception ex)
            {
                apiResponses.Errors.Add("Internal Server Error");
                apiResponses.StatusCode = 500;
            }

            return apiResponses;
        }

        public async Task<ApiResponses> RetrieveAccount(int accountId)
        {
            ApiResponses apiResponses = new ApiResponses();

            try
            {
                apiResponses.Errors = null;
                apiResponses.Result = _accountRepository.Get(accountId);
                apiResponses.StatusCode = 200;
            }
            catch (Exception ex)
            {
                apiResponses.Errors.Add("Internal Server Error");
                apiResponses.StatusCode = 500;
            }

            return apiResponses;
        }

        public async Task<ApiResponses> RetrieveAllAccounts()
        {
            ApiResponses apiResponses = new ApiResponses();

            try
            {
                apiResponses.Errors = null;
                apiResponses.Result = _accountRepository.GetAll();
                apiResponses.StatusCode = 200;
            }
            catch (Exception ex)
            {
                apiResponses.Errors.Add("Internal Server Error");
                apiResponses.StatusCode = 500;
            }

            return apiResponses;
        }

        public async Task<ApiResponses> SaveAccount(AccountDto accountDto)
        {
            ApiResponses apiResponses = new ApiResponses();
            
            try
            {

                ValidationResult result = await _validator.ValidateAsync(accountDto);

                if (!result.IsValid)
                {

                    foreach (var error in result.Errors)
                    {
                        apiResponses.Errors.Add(error.ErrorMessage);
                    }

                    throw new AccountValidationException();
                }
                else
                {
                    Account _account = _mapper.Map<Account>(accountDto);

                    var isDuplicate = _accountRepository.GetByName(_account.Name);

                    if (isDuplicate != null)
                    {
                        throw new DuplicateAccountException($"Account '{_account.Name}' already Exists");
                    }

                    _accountRepository.Add(_account);

                    apiResponses.Errors = null;
                    apiResponses.Result = _account;
                    apiResponses.StatusCode = 200;
                }

            }
            catch (DuplicateAccountException ex)
            {
                apiResponses.Errors.Add(ex.Message);
                apiResponses.StatusCode = 400;
            }
            catch (AccountValidationException)
            {
                apiResponses.StatusCode = 400;
            }
            catch (Exception ex)
            {
                apiResponses.Errors.Add("Internal Server Error");
                apiResponses.StatusCode = 500;
            }

            return apiResponses;
        }

    }
}
