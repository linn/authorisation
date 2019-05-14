namespace Linn.Authorisation.IoC
{
    using Autofac;

    using Linn.Common.Logging;

    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
#if DEBUG
            builder.RegisterType<ConsoleLog>().As<ILog>().SingleInstance();
#else
            builder.Register(c => new AmazonSqsLog(c.Resolve<IAmazonSQS>(), LoggingConfiguration.Environment, LoggingConfiguration.MaxInnerExceptionDepth, LoggingConfiguration.AmazonSqsQueueUri))
                .As<ILog>()
                .SingleInstance();
#endif
        }
    }
}
