using SurveyBasket.API.Abstractions;
using SurveyBasket.API.Contracts.Poll;

namespace SurveyBasket.API.Services
{
    public interface IPollService
    {
        Task<Result<IEnumerable<PollDto>>> GetAllPolls(CancellationToken CancellationToken = default); 

        Task<Result<PollDto> >GetPollById(int id ,  CancellationToken cancellationToken = default);

        Task<Result<PollDto>> AddPoll(PollViewModel model, CancellationToken cancellationToken = default);
    }
}
