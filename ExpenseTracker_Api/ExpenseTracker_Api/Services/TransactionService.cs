using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Serialization;
using AutoMapper;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.CustomException;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;
using FluentValidation;
using FluentValidation.Results;

namespace ExpenseTracker_Api.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private IValidator<TransactionDto> _validator;
    private IMapper _mapper;

    public TransactionService(ITransactionRepository transactionRepository, IValidator<TransactionDto> validator, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task<ApiResponses> SaveTransaction(TransactionDto transactionDto)
    {
        ApiResponses apiResponses = new ApiResponses();

        try
        {
            ValidationResult result = _validator.Validate(transactionDto);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    apiResponses?.Errors?.Add(error.ErrorMessage);
                }

                throw new TransactionValidationException();
            }
            else
            {
                Transaction transaction = _mapper.Map<Transaction>(transactionDto);
                
                var isDuplicate = _transactionRepository.GetTransaction(transactionDto);
                
                if (isDuplicate != null)
                {
                    throw new DuplicateTransactionException($"Transaction already Exists");
                }
                
                _transactionRepository.Add(transaction);
                
                apiResponses.Errors = null;
                apiResponses.Result = transaction;
                apiResponses.StatusCode = 200;
            }
        }
        catch (DuplicateTransactionException ex)
        {
            apiResponses.Errors.Add(ex.Message);
            apiResponses.StatusCode = 400;
        }
        catch (TransactionValidationException)
        {
            apiResponses.StatusCode = 400;
        }
        catch (Exception wx)
        {
            apiResponses.Errors.Add("Internal Server Error");
            apiResponses.StatusCode = 500;
        }

        return apiResponses;
    }

    public async Task<ApiResponses> ModifyTransaction(int transactionId, TransactionDto transactionDto)
    {
        ApiResponses apiResponses = new ApiResponses();

        try
        {
            //Check if the account id exists
            var transaction = _transactionRepository.Get(transactionId);

            if (transaction.Id != transactionId)
       
                apiResponses.Errors.Add("Invalid transaction");

            ValidationResult result = await _validator.ValidateAsync(transactionDto);

            if (!result.IsValid)
            {

                foreach (var error in result.Errors)
                {
                    apiResponses.Errors.Add(error.ErrorMessage);
                }

                throw new TransactionValidationException();
            }
            else
            {
                transaction.Id = transactionId; 
                transaction.Description = transactionDto.Description;
                transaction.Amount = transactionDto.Amount;
                transaction.AccountId = transactionDto.AccountId;
                transaction.TransactionDate = transactionDto.TransactionDate;

                _transactionRepository.Update(transaction);

                apiResponses.Errors = null;
                apiResponses.Result = transaction;
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

    public async Task<ApiResponses> RemoveTransaction(int transactionId)
    {
         ApiResponses apiResponses = new ApiResponses();

            try
            {
                 _transactionRepository.Delete(transactionId);
                
                apiResponses.Errors = null;
                apiResponses.Result = $"Transaction ({transactionId}) has been removed"; 
                apiResponses.StatusCode = 200;
            }
            catch (Exception ex)
            {
                apiResponses.Errors.Add("Internal Server Error");
                apiResponses.StatusCode = 500;
            }

            return apiResponses;
    }

    public Task<ApiResponses> RetrieveTransaction(int transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponses> RetrieveAllTransaction(TransactionSearchDto searchDto)
    {
        ApiResponses apiResponses = new ApiResponses();

            try
            {
                apiResponses.Errors = null;
                apiResponses.Result = new { item =  _transactionRepository.GetAll(searchDto.PageNumber , searchDto.PageSize,searchDto.SearchTerm,searchDto.FromDate,searchDto.ToDate, searchDto.AccountId)};
                apiResponses.StatusCode = 200;
            }
            catch (Exception ex)
            {
                apiResponses.Errors.Add("Internal Server Error");
                apiResponses.StatusCode = 500;
            }

            return apiResponses;
    }
}