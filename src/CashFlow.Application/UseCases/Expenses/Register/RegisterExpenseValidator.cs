﻿using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The amount must be greater than 0");
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expenses cannot be on the future");
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment type is not valid");
    }
}
