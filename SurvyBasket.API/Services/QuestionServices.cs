using SurveyBasket.API.Contracts.Question;
using System.Collections.Generic;

namespace SurveyBasket.API.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IGenericRespository<Poll> _pollRepository;
        private readonly IGenericRespository<Question> _questionRepository;

        public QuestionServices(IGenericRespository<Poll> pollRepository ,
            IGenericRespository<Question> questionRepository )
        {
            _pollRepository = pollRepository;
            _questionRepository = questionRepository;
        }


        public async Task<Result<QuestionDto>> AddQuestion(int pollId, QuestionVewModel model, CancellationToken cancellationToken = default)
        {
            var isPollExist = await _pollRepository.IsEntityExsit(x => x.ID == pollId, cancellationToken);
            if (!isPollExist)
                return Result.Failure<QuestionDto>(PollErrors.PollNotFound);

            var isQuestionExsit = await _questionRepository.
                                            IsEntityExsit(x => x.Content == model.Content && x.PollId == pollId, cancellationToken);
        
                if(isQuestionExsit)
                return Result.Failure<QuestionDto>(QuestionErrors.QuestionDeplucated);

            var questionInDb = model.Adapt<Question>();
            questionInDb.PollId = pollId;


            var response = await _questionRepository.AddAsync(questionInDb);

            return Result.Success(response.Adapt<QuestionDto>());

        }


        public async Task<Result<IEnumerable<QuestionDto>>> GetAllQuestion(int pollId, CancellationToken cancellationToken = default)
        {
            var isPollExsit = await _pollRepository.IsEntityExsit(x => x.ID == pollId, cancellationToken);
            if (!isPollExsit)
                return Result.Failure<IEnumerable<QuestionDto>>(PollErrors.PollNotFound);

           var questionIndb = await _questionRepository.GetAllAsync(cancellationToken)
                                                                              .Where(x => x.PollId == pollId && !x.IsDelated)
                                                                              .Include(x => x.Answers)
                                                                              .ProjectToType<QuestionDto>()
                                                                              .AsNoTracking().
                                                                              ToListAsync(cancellationToken: cancellationToken);

            if(questionIndb is null)
                return Result.Failure<IEnumerable<QuestionDto>>(PollErrors.PollNotFound);

            return Result.Success(questionIndb.Adapt<IEnumerable<QuestionDto>>());

        }

        public async Task<Result<QuestionDto>> GetQuestionById(int pollId, int Id, CancellationToken cancellationToken = default)
        {
           var questionToReutrn =  await  _questionRepository.GetAllAsync(cancellationToken).
                                        Where(x => x.ID == Id && x.PollId == pollId)
                                         .Include(x => x.Answers)
                                         .ProjectToType<QuestionDto>()
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(cancellationToken: cancellationToken);


            if (questionToReutrn is null)
                return Result.Failure<QuestionDto>(QuestionErrors.QuestionNotFound);

            return Result.Success(questionToReutrn);

        }


        public async Task<Result> ToggelStatus(int id, int pollId, CancellationToken cancellationToken = default)
        {
            var question = await _questionRepository.GetAllAsync(cancellationToken)
                                                                              .Where(x => x.ID == id && x.PollId == pollId)
                                                                              .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (question is null)
                return Result.Failure(QuestionErrors.QuestionNotFound);

            question.IsActive = !question.IsActive;

            return Result.Success();

        }


        public Task<Result> HardDelteQuestion(int pollId, int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result> SoftDelteQuestion(int pollId, int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }



        public Task<Result> UpdateQuestion(int pollid, int id, QuestionVewModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
