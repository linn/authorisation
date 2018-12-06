namespace Linn.Authorisation.Messaging.Host
{
    using Autofac;

    using Linn.Authorisation.IoC;
    using Linn.Common.Messaging.RabbitMQ.Autofac;

    public static class Configuration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AmazonCredentialsModule>();
            builder.RegisterModule<AmazonSqsModule>();
            builder.RegisterModule<LoggingModule>();
            //builder.RegisterModule<MessagingModule>();
            //builder.RegisterModule<PersistenceModule>();
            //builder.RegisterModule<ServiceModule>();
            builder.RegisterReceiver("authorisation.q", "authorisation.dlx");

            builder.RegisterType<Listener>().AsSelf();

            return builder.Build();
        }
    }
}
