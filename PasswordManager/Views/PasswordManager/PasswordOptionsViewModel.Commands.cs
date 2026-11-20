using ReactiveUI;

namespace PasswordManager.Client.Views
{
    public sealed partial class PasswordOptionsViewModel
    {
        public sealed class PasswordOptionsViewModelCommands
        {
            public PasswordOptionsViewModelCommands(PasswordOptionsViewModel vm)
            {
                System.Diagnostics.Debug.WriteLine("--> [Client] PasswordOptionsViewModelCommands initialized!");

                CopyAllPasswordsToClipboard = ReactiveCommand.CreateFromTask(async () =>
                {
                    System.Diagnostics.Debug.WriteLine("--> [Client] CopyAllPasswordsToClipboard command executed!");
                    await vm.CopyAllPasswordsToClipboard();
                });
            }

            public IReactiveCommand CopyAllPasswordsToClipboard { get; }
        }

        private PasswordOptionsViewModelCommands _commands;

        public PasswordOptionsViewModelCommands Commands => _commands ??= new(this);
    }
}