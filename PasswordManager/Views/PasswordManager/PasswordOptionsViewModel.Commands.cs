using ReactiveUI;

namespace PasswordManager.Client.Views
{
    public sealed partial class PasswordOptionsViewModel
    {
        public sealed class PasswordOptionsViewModelCommands
        {
            public PasswordOptionsViewModelCommands(PasswordOptionsViewModel vm)
            {
                GeneratePassword = ReactiveCommand.Create(() =>
                {
                    vm.GeneratePassword();
                });
            }

            public IReactiveCommand GeneratePassword { get; }
        }

        private PasswordOptionsViewModelCommands _commands;

        public PasswordOptionsViewModelCommands Commands => _commands ??= new(this);
    }
}