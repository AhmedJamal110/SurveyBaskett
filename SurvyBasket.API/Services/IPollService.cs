using SurveyBasket.API.Abstractions;
using SurveyBasket.API.Contracts.Poll;

namespace SurveyBasket.API.Services
{
    public interface IPollService
    {
        Task<Result<IEnumerable<PollDto>>> GetAllPolls(CancellationToken CancellationToken = default); 
         Task<Result<PollDto> >GetPollById(int id ,  CancellationToken cancellationToken = default);
        Task<Result<PollDto>> AddPoll(PollViewModel model, CancellationToken cancellationToken = default);
        Task<Result> UpdatePoll(int id ,  PollViewModel model, CancellationToken cancellationToken = default);
        Task<Result> HardDeltePoll(int id, CancellationToken cancellationToken = default);
        Task<Result> SoftDeltePoll(int id,  CancellationToken cancellationToken = default);
        Task<Result> ToggelStatus (int id,  CancellationToken cancellationToken = default);
  
    
    }
}
