using Autofac;
using PasswordManager.Client.Services;

namespace PasswordManager.Client.Modules
{
    internal class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PasswordGeneratorService>()
                .As<IPasswordGeneratorService>()
                .SingleInstance();

            builder
                .RegisterType<JsonPasswordStorageService>()
                .As<IPasswordStorageService>()
                .SingleInstance();

            builder
                .RegisterType<PasswordEventService>()
                .As<IPasswordEventService>()
                .SingleInstance();
        }
    }
}