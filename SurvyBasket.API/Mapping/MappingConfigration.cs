namespace SurveyBasket.API.Mapping
{
    public class MappingConfigration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Poll, PollDto>()
                .Map(dis => dis.ID, src => src.ID);
        }
    }
}
