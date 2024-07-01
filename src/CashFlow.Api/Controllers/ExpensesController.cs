using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

public class ExpensesController : CashFlowBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
    {
        try
        {
            var response = new RegisterExpenseUseCase().Execute(request);

            return Created(string.Empty, response);
        }
        catch (ArgumentException ex)
        {
            var errorMessage = new ResponseErrorJson
            {
                ErrorMessage = ex.Message
            };

            return BadRequest(errorMessage);
        }
        catch
        {
            var errorMessage = new ResponseErrorJson
            {
                ErrorMessage = "Unknown Error"
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }
    }
}
