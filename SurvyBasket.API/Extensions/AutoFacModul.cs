using Autofac;
using SurveyBasket.API.JwtService;
using SurveyBasket.API.Services;
using Module = Autofac.Module;

namespace SurveyBasket.API.Extensions
{
    public class AutoFacModul : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRespository<>)).As(typeof(IGenericRespository<>)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(PollService)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType(typeof(JwtProvider)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType(typeof(AuthService)).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
