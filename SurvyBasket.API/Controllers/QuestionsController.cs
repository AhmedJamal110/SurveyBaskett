using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.API.Contracts.Question;
using SurveyBasket.API.Services;
using System.Runtime.InteropServices;

namespace SurveyBasket.API.Controllers
{
    [Route("api/poll/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionServices _questionServices;

        public QuestionsController(IQuestionServices questionServices)
        {
            _questionServices = questionServices;
        }

        [HttpPost("create-question")]
        public async Task<ActionResult> AddQuestion([FromRoute] int pollId , QuestionVewModel model , CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.AddQuestion(pollId, model, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);

            return result.Error.Equals(QuestionErrors.QuestionDeplucated)
              ? result.ToProblem(StatusCodes.Status409Conflict)
              : result.ToProblem(StatusCodes.Status404NotFound);
                
        }



        [HttpGet("")]
        public async Task<ActionResult> GetQuestions([FromRoute] int pollId, CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.GetAllQuestion(pollId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpGet("question/{id}")]
        public async Task<ActionResult> GetQuestionByID([FromRoute] int id, int pollId , CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.GetQuestionById(id ,  pollId, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpPut("toggel-action/{id}")]
        public async Task<ActionResult> ToggelActions([FromRoute] int id, int pollId, CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.ToggelStatus(id, pollId, cancellationToken);

            return result.IsSuccess ? Ok() : result.ToProblem(StatusCodes.Status404NotFound);
        }

    }
}
