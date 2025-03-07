﻿using ExpenseTracker_Api.Models.DTO;
using FluentValidation;

namespace ExpenseTracker_Api.Services.Validator.DTO
{
    public class AccountDTOValidator : AbstractValidator<AccountDto>
    {
        public AccountDTOValidator()
        {
            RuleFor(account => account.Name).NotEmpty().MaximumLength(255);
        }
    }
}
