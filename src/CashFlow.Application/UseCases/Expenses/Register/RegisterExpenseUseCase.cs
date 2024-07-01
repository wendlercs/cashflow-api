using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        return new ResponseRegisterExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);

        if(titleIsEmpty)
        {
            throw new ArgumentException("The title is required");
        }

        if(request.Amount <= 0)
        {
            throw new ArgumentException("The amount must be greater than zero");
        }

        var dateResult = DateTime.Compare(request.Date, DateTime.UtcNow);

        if(dateResult > 0)
        {
            throw new ArgumentException("The expense date cannot be on the future");
        }

        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentTypeEnum), request.PaymentType);

        if(!paymentTypeIsValid)
        {
            throw new ArgumentException("Payment type is not valid");
        }

    }
}
