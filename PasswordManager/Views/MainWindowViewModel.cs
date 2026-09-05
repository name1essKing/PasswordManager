using PasswordManager.ui;
using PasswordManager.Client.Views;

namespace PasswordManager.Client.Views
{
    public sealed partial class MainWindowViewModel : ViewModelBase
    {
        public PasswordOptionsViewModel PasswordOptionsViewModel { get; }
        public PasswordResultsViewModel PasswordResultsViewModel { get; }

        public MainWindowViewModel(PasswordOptionsViewModel passwordOptionsViewModel, PasswordResultsViewModel passwordResultsViewModel)
        {
            PasswordOptionsViewModel = passwordOptionsViewModel;
            PasswordResultsViewModel = passwordResultsViewModel;
        }
    }
}
