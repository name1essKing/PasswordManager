using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using PasswordManager.Client.Services;
using PasswordManager.ui;
using ReactiveUI;

namespace PasswordManager.Client.Views
{
    public sealed partial class PasswordOptionsViewModel : ViewModelBase
    {
        // Сервисы
        private readonly IPasswordStorageService _storage;
        private readonly IPasswordSettingsService _settingsService;

        /// <summary>
        /// Длина пароля, которую пользователь хочет сгенерировать. Значение по умолчанию - 8.
        /// </summary>
        public int PasswordLength
        {
            get => _settingsService.PasswordLength;
            set
            {
                if (_settingsService.PasswordLength != value)
                {
                    _settingsService.PasswordLength = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// флаг, указывающий, следует ли включать цифры в сгенерированный пароль. Значение по умолчанию - true.
        /// </summary>
        public bool UseDigit
        {
            get => _settingsService.UseDigit;
            set
            {
                if (_settingsService.UseDigit != value)
                {
                    _settingsService.UseDigit = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// флаг, указывающий, следует ли включать символы в сгенерированный пароль. Значение по умолчанию - true.
        /// </summary>
        public bool UseSymbols
        {
            get => _settingsService.UseSymbols;
            set
            {
                if (_settingsService.UseSymbols != value)
                {
                    _settingsService.UseSymbols = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// флаг, указывающий, следует ли включать заглавные буквы в сгенерированный пароль. Значение по умолчанию - true.
        /// </summary>
        public bool UseUpperCase
        {
            get => _settingsService.UseUpperCase;
            set
            {
                if (_settingsService.UseUpperCase != value)
                {
                    _settingsService.UseUpperCase = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// количество паролей, которые пользователь хочет сгенерировать. Значение по умолчанию - 1.
        /// </summary>
        public int PasswordsCount
        {
            get => _settingsService.PasswordsCount;
            set
            {
                if (_settingsService.PasswordsCount != value)
                {
                    _settingsService.PasswordsCount = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public PasswordOptionsViewModel(IPasswordStorageService storage, IPasswordSettingsService settingsService)
        {
            _storage = storage;
            _settingsService = settingsService;

            var savedData = _storage.LoadHistory();

            if (savedData.Options == PasswordOptions.None)
            {
                UseDigit = true;
                UseSymbols = true;
                UseUpperCase = true;
            }
            else
            {
                UseDigit = savedData.Options.HasFlag(PasswordOptions.Digits);
                UseSymbols = savedData.Options.HasFlag(PasswordOptions.Symbols);
                UseUpperCase = savedData.Options.HasFlag(PasswordOptions.UpperCase);
            }

            PasswordLength = 8;
            PasswordsCount = 1;
        }

        /// <summary>
        /// Скопировать все сгенерированные пароли в буфер обмена
        /// </summary>
        public async Task CopyAllPasswordsToClipboard()
        {
            var savedData = _storage.LoadHistory();
            if (savedData.History == null || savedData.History.Count == 0) return;

            var allPasswords = string.Join(Environment.NewLine, savedData.History);

            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var clipboard = desktop.MainWindow?.Clipboard;
                if (clipboard != null)
                {
                    await clipboard.SetTextAsync(allPasswords);
                }
            }
        }
    }
}