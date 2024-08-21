using Microsoft.AspNetCore.Mvc;
using SurveyBasket.API.Abstractions;

namespace SurveyBasket.API.Extensions
{
    public static class ResultExtension
    {
        public static ObjectResult ToProblem(this Result result , int statusCode)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("Cant convert success result to problem");


            var problem =  Results.Problem(statusCode: statusCode);
            var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;
          
            problemDetails!.Extensions = new Dictionary<string, object?>
            {
                {
                    "errors" , new[] {result.Error}
                }
            };

            return new ObjectResult(problemDetails);
        }
    }
}
