using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using WebCalcApi.Evaluations;

namespace WebCalcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcController : ControllerBase
    {
        [HttpGet]
        [Route("Plus")]
        public IActionResult PlusOperator([FromQuery] ExpressionData expression)
        {
            return Ok(new ExpressionResult(expression,expression.Operand1 + expression.Operand2));
        }

        [HttpGet]
        [Route("Minus")]
        public IActionResult MinusOperator([FromQuery] ExpressionData expression)
        {
            return Ok(new ExpressionResult(expression, expression.Operand1 - expression.Operand2));
        }

        [HttpGet]
        [Route("Multiply")]
        public IActionResult MultiplyOperator([FromQuery] ExpressionData expression)
        {
            return Ok(new ExpressionResult(expression, expression.Operand1 * expression.Operand2));
            
        }

        [HttpGet]
        [Route("Divide")]
        public IActionResult DivideOperator([FromQuery] ExpressionData expression)
        {
            if (expression.Operand2 == 0) return BadRequest("Cannot Divide by zero");

            return Ok(new ExpressionResult(expression, expression.Operand1 / expression.Operand2));
        }

        [HttpGet]
        [Route("Pow")]
        public IActionResult PowOperator([FromQuery] ExpressionData expression)
        {
            return Ok(new ExpressionResult(expression, float.Parse(Math.Pow(expression.Operand1,expression.Operand2).ToString())));
        }

        [HttpGet]
        [Route("Root")]
        public IActionResult RootOperator([FromQuery] ExpressionData expression)
        {
            return Ok(new ExpressionResult(expression, float.Parse(Math.Pow(expression.Operand1, 1 / expression.Operand2).ToString())));
        }

        [HttpPost]
        [Route("CustomExp")]
        public IActionResult CustomExpression([FromBody] CustomExpressionData expression)
        {

            (bool isValidExpression, string? expressionTrouble) = StringExpCheck.Check(expression.Expression);

            if (isValidExpression)
            {
                try
                {
                    var res = StringExpEval.Eval(expression.Expression);
                    return Ok(new CustomExpressionResult(expression, res));
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(expressionTrouble);
            }

        }



    }
}
