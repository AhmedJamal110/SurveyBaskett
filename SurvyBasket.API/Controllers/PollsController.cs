using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.API.Services;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;

        public PollsController(IPollService pollService )
        {
            _pollService = pollService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetPolls(CancellationToken cancellationToken)
        {
            var result = await _pollService.GetAllPolls(cancellationToken);

            return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
        }

        [HttpGet("poll/{id}")]
        public async Task<ActionResult> GetPollById(int id ,  CancellationToken cancellationToken)
        {
            var result = await _pollService.GetPollById(id , cancellationToken);

            return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
        }

        [HttpPost("ceate-poll")]
        public async Task<ActionResult> AddPoll(PollViewModel model, CancellationToken cancellationToken)
        {
            var result = await _pollService.AddPoll( model, cancellationToken);
           

            return result.IsFailure  ? BadRequest(result.Error) :  Ok(result.Value);
        }

        [HttpPut("update-poll/{id}")]
        public async Task<ActionResult> UpdatePoll([FromRoute] int id ,  PollViewModel model, CancellationToken cancellationToken)
        {
            var result = await _pollService.UpdatePoll(id , model, cancellationToken);


            return result.IsFailure ? NotFound(result.Error) : NoContent();
        }

        [HttpDelete("hard-delete-poll/{id}")]
        public async Task<ActionResult> HardDeletePoll([FromRoute] int id, CancellationToken cancellationToken)
        {
            
            var result = await _pollService.HardDeltePoll(id , cancellationToken)  ;

            return result.IsFailure ? NotFound(result.Error) : NoContent();
        }

        [HttpDelete("soft-delete-poll/{id}")]
        public async Task<ActionResult> SoftDeletePoll([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.SoftDeltePoll(id, cancellationToken);
            return result.IsFailure ? NotFound(result.Error) : NoContent();
        }

        [HttpPut("toggel/{id}")]
        public async Task<ActionResult> ToggelStatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.ToggelStatus(id, cancellationToken);

            return result.IsFailure ? NotFound(result.Error) : NoContent();
        }

    }
}
