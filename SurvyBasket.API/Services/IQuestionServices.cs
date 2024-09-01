using SurveyBasket.API.Contracts.Question;

namespace SurveyBasket.API.Services
{
    public interface IQuestionServices
    {
        Task<Result<IEnumerable<QuestionDto>>> GetAllQuestion(int pollId , CancellationToken cancellationToken = default);
        Task<Result<QuestionDto>> GetQuestionById(int pollId ,  int Id , CancellationToken cancellationToken = default);
        Task<Result<QuestionDto>> AddQuestion(int pollId ,  QuestionVewModel model ,  CancellationToken cancellationToken = default);
        Task<Result> SoftDelteQuestion(int pollId ,  int id ,  CancellationToken cancellationToken = default);
        Task<Result> HardDelteQuestion(int pollId ,  int id , CancellationToken cancellationToken = default);
        Task<Result> UpdateQuestion(int pollid ,  int id, QuestionVewModel model ,CancellationToken cancellationToken = default);
        Task<Result> ToggelStatus(int id ,  int pollId, CancellationToken cancellationToken = default);
    }
}
