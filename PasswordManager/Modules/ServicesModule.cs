using System;
using System.Net.Http;
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

            builder
                .RegisterType<PasswordSettingsService>()
                .As<IPasswordSettingsService>()
                .SingleInstance();

            builder.Register(c => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5297/")
            }).SingleInstance();

            builder
                .RegisterType<PasswordApiClient>()
                .As<IPasswordApiClient>()
                .SingleInstance();
        }
    }
}