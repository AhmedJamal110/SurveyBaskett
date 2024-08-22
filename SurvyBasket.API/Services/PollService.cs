namespace SurveyBasket.API.Services
{
    public class PollService : IPollService
    {
        private readonly IGenericRespository<Poll> _pollRepository;

        public PollService(IGenericRespository<Poll> pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public async Task<Result<IEnumerable<PollDto>>> GetAllPolls(CancellationToken CancellationToken = default)
        {
            var pollsInDb = await _pollRepository.GetAllAsync(CancellationToken).AsNoTracking().ToListAsync(cancellationToken: CancellationToken);
            if (pollsInDb is null)
                return Result.Failure<IEnumerable<PollDto>>(PollErrors.PollNotFound);

            return Result.Success(pollsInDb.Adapt<IEnumerable<PollDto>>());
        }

        public async Task<Result<PollDto>> GetPollById(int id, CancellationToken cancellationToken = default)
        {
            var pollInDb = await _pollRepository.GetByIdAsync(id, cancellationToken);
            if (pollInDb is null)
                return Result.Failure<PollDto>(PollErrors.PollNotFound);

            return Result.Success(pollInDb.Adapt<PollDto>());
            
        }

        public async Task<Result<PollDto>> AddPoll(PollViewModel model, CancellationToken cancellationToken = default)
        {
            var poll = model.Adapt<Poll>();
             var isExist = await _pollRepository.IsEntityExsit(x => x.Title == model.Title, cancellationToken);
                   if (isExist)
                         return Result.Failure<PollDto>(PollErrors.PollDeplucated);

            var result = await _pollRepository.AddAsync(poll, cancellationToken);
            if (result is null)
                return Result.Failure<PollDto>(PollErrors.PollNotFound);

            return Result.Success<PollDto>(result.Adapt<PollDto>());
        }
        public async Task<Result> UpdatePoll(int id, PollViewModel model, CancellationToken cancellationToken = default)
        {
            var pollInDb = await _pollRepository.GetByIdAsync(id, cancellationToken);
                 if (pollInDb is null)
                         return Result.Failure(PollErrors.PollNotFound);

            var isExsit = await _pollRepository.IsEntityExsit(x => x.Title == model.Title && x.ID != id, cancellationToken);
            if (isExsit)
                return Result.Failure(PollErrors.PollDeplucated);

            pollInDb.Title = model.Title; 
            pollInDb.Summary = model.Summary; 
            pollInDb.StartsAt = model.StartsAt; 
            pollInDb.EndsAt = model.EndsAt;
             await _pollRepository.UpdateAsync(pollInDb, cancellationToken);

            return Result.Success();
        }

        public async Task<Result> HardDeltePoll(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _pollRepository.GetByIdAsync(id, cancellationToken);
                   if (poll is null)
                            return Result.Failure(PollErrors.PollNotFound);
                
              await _pollRepository.HardDelteAsync(id, cancellationToken);
                
            return Result.Success();
        }

        public async Task<Result> SoftDeltePoll(int id, CancellationToken cancellationToken = default)
        {
            var isExsit = await _pollRepository.IsEntityExsit(x => x.ID == id, cancellationToken);
            if (!isExsit)
                return Result.Failure(PollErrors.PollNotFound);

             await _pollRepository.SoftDelteAsync(id, cancellationToken);


            return Result.Success();
        }

        public async Task<Result> ToggelStatus(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _pollRepository.GetByIdAsync(id, cancellationToken);
            if(poll is null )
                return Result.Failure(PollErrors.PollNotFound);
            poll.IsPublished = !poll.IsPublished;
           
            return Result.Success();
        }




    }
}
