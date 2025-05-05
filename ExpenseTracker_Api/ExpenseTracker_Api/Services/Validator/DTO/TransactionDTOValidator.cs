using System.Globalization;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;
using FluentValidation;

namespace ExpenseTracker_Api.Services.Validator.DTO;

public class TransactionDtoValidator : AbstractValidator<TransactionDto>
{
    
    public TransactionDtoValidator()
    {
        RuleFor(transaction => transaction.Description).NotEmpty().MaximumLength(255);
        RuleFor(transaction => transaction.AccountId).NotEmpty().NotNull().GreaterThan(0);
        RuleFor(transaction => transaction.Amount).Must(BeAValidateAmount).WithMessage("Please enter valid amount");
    }

    private bool BeAValidateAmount(decimal amount)
    {
        if(decimal.TryParse(amount.ToString(),NumberStyles.Currency, CultureInfo.InvariantCulture, out _))
           return true;
        else
            return false;
    }
}