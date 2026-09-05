using Autofac;
using PasswordManager.Client.Services;
using PasswordManager.Client.Views;

namespace PasswordManager.Client.Modules
{
    internal class ViewModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MainWindowView>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<MainWindowViewModel>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<PasswordOptionsViewModel>()
                .AsSelf();

            builder
                .RegisterType<PasswordResultsViewModel>()
                .AsSelf();
        }
    }
}