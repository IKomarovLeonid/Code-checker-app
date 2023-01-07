using Autofac;
using Processing.Listeners;
using Processing.Logic;
using Processing.Parsing;
using Processing.Workers;

namespace API.Ioc
{
    internal class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CodeParser>().AsSelf().SingleInstance();
            builder.RegisterType<TestsWorker>().AsSelf().SingleInstance();
            builder.RegisterType<TestsRunner>().AsSelf().SingleInstance();
            builder.RegisterType<SolutionsListener>().AsSelf().SingleInstance();
        }
    }
}
