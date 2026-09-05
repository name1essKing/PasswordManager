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
                CopyPasswordToClipboard = ReactiveCommand.CreateFromTask<string>(
                    async (password) =>
                    {
                        await vm.CopyPasswordToClipboard(password);
                    }
                );

                CopyAllPasswordsToClipboard = ReactiveCommand.CreateFromTask(
                    async () =>
                    {
                        await vm.CopyAllPasswordsToClipboard();
                    }
                );
            }

            public ReactiveCommand<string, Unit> CopyPasswordToClipboard { get; }

            public IReactiveCommand CopyAllPasswordsToClipboard { get; }
        }

        private PasswordResultsViewModelCommands _commands;

        public PasswordResultsViewModelCommands Commands => _commands ??= new(this);
    }
}