namespace CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidateException : CashFlowException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidateException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
