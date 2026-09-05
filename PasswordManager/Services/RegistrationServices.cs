using Autofac;

namespace PasswordManager.Client.Services
{
    /// <summary>
    /// The registration service.
    /// </summary>
    static class RegistrationService
    {
        /// <summary>
        /// Creates the container.
        /// </summary>
        /// <param name="appServices">The app services.</param>
        /// <returns>An IContainer.</returns>
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(PasswordManager.Client.App).Assembly);

            var container = builder.Build();

            return container;
        }
    }
}