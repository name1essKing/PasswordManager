using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using PasswordManager.Client.Services;
using PasswordManager.ui;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PasswordManager.Client.Views
{
    public sealed partial class PasswordResultsViewModel : ViewModelBase
    {
        // Сервисы
        private readonly IPasswordStorageService _storage;
        private readonly IPasswordEventService _eventService;

        /// <summary>
        /// Коллекция для вывода сгенерированных паролей в UI
        /// </summary>
        public ObservableCollection<string> GeneratedPasswords { get; } = new();
        public PasswordResultsViewModel(
            IPasswordStorageService storage,
            IPasswordEventService eventService)
        {
            _storage = storage;
            _eventService = eventService;

            var savedData = _storage.LoadHistory();
            GeneratedPasswords = new ObservableCollection<string>([.. savedData.History]);

            _eventService.PasswordsGenerated += OnPasswordsGenerated;
        }

        private void OnPasswordsGenerated(List<string> passwords)
        {
            GeneratedPasswords.Clear();
            foreach (var password in passwords)
            {
                GeneratedPasswords.Add(password);
            }
        }

        /// <summary>
        /// Копирование пароля в буфер обмена
        /// </summary>
        public async Task CopyPasswordToClipboard(string? password)
        {
            if (string.IsNullOrEmpty(password)) return;

            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var clipboard = desktop.MainWindow?.Clipboard;

                if (clipboard != null)
                {
                    await clipboard.SetTextAsync(password);
                }
            }
        }

        /// <summary>
        /// Скопировать все сгенерированные пароли в буфер обмена
        /// </summary>
        public async Task CopyAllPasswordsToClipboard()
        {
            if (GeneratedPasswords.Count == 0) return;

            var allPasswords = string.Join(Environment.NewLine, GeneratedPasswords);

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