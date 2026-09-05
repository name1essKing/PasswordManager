using Avalonia;
using Avalonia.ReactiveUI;
using PasswordManager.Client;
using System;

namespace PasswordManager
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect() 
                .UseReactiveUI()     
                .WithInterFont()     
                .LogToTrace();       
    }
}