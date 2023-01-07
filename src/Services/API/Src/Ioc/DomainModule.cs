using Autofac;
using Database;
using Objects.Dto;

namespace API.Ioc
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Storage<CodeTaskDto>>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<Storage<CodeSolutionDto>>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
