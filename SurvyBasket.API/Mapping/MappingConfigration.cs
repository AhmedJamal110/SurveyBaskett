using SurveyBasket.API.Contracts.Question;

namespace SurveyBasket.API.Mapping
{
    public class MappingConfigration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Poll, PollDto>()
                .Map(dis => dis.ID, src => src.ID);

            config.NewConfig<QuestionVewModel, Question>()
                .Map(dis => dis.Answers, src => src.Answers.Select(answer => new Answer { Content = answer }));

        }
    }
}
