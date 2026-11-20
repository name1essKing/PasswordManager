using ReactiveUI;
using System.Reactive;

namespace PasswordManager.Client.Views
{
    public sealed partial class PasswordResultsViewModel
    {
        public sealed class PasswordResultsViewModelCommands
        {
            public PasswordResultsViewModelCommands(PasswordResultsViewModel vm)
            {
                GeneratePassword = ReactiveCommand.CreateFromTask(async () =>
                {
                    System.Diagnostics.Debug.WriteLine("--> [Client] GeneratePassword command executed!");
                    await vm.GeneratePassword();
                });

                CopyPasswordToClipboard = ReactiveCommand.CreateFromTask<string>(
                    async (password) =>
                    {
                        await vm.CopyPasswordToClipboard(password);
                    }
                );
            }

            public IReactiveCommand GeneratePassword { get; }
            public ReactiveCommand<string, Unit> CopyPasswordToClipboard { get; }
        }

        private PasswordResultsViewModelCommands _commands;

        public PasswordResultsViewModelCommands Commands => _commands ??= new(this);
    }
}