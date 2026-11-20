using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using PasswordManager.Client.Services;
using PasswordManager.Dto.DTOs;
using PasswordManager.ui;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PasswordManager.Client.Views
{
    /// <summary>
    /// ViewModel экрана результатов генерации паролей и синхронизации с API.
    /// </summary>
    public sealed partial class PasswordResultsViewModel : ViewModelBase
    {
        private readonly IPasswordGeneratorService _generator;
        private readonly IPasswordStorageService _storage;
        private readonly IPasswordEventService _eventService;
        private readonly IPasswordApiClient _apiClient;
        private readonly IPasswordSettingsService _settingsService;

        /// <summary>
        /// Коллекция для вывода сгенерированных паролей в UI.
        /// </summary>
        public ObservableCollection<string> GeneratedPasswords { get; } = new();

        public PasswordResultsViewModel(
            IPasswordGeneratorService generator,
            IPasswordStorageService storage,
            IPasswordEventService eventService,
            IPasswordApiClient apiClient,
            IPasswordSettingsService settingsService)
        {
            _generator = generator;
            _storage = storage;
            _eventService = eventService;
            _apiClient = apiClient;
            _settingsService = settingsService;

            var savedData = _storage.LoadHistory();
            if (savedData.History != null)
            {
                foreach (var item in savedData.History)
                {
                    GeneratedPasswords.Add(item);
                }
            }

            _eventService.PasswordsGenerated += OnPasswordsGenerated;
        }

        /// <summary>
        /// Генерирует пароли по выбранным настройкам, отправляет пачку на сервер и сохраняет локальную историю.
        /// </summary>
        public async Task GeneratePassword()
        {
            var options = PasswordOptions.None;
            if (_settingsService.UseDigit) options |= PasswordOptions.Digits;
            if (_settingsService.UseSymbols) options |= PasswordOptions.Symbols;
            if (_settingsService.UseUpperCase) options |= PasswordOptions.UpperCase;

            var count = _settingsService.PasswordsCount;
            var length = _settingsService.PasswordLength;

            await Task.Run(async () =>
            {
                var generatedList = new List<string>();
                var dtoList = new List<CreatePasswordDto>();

                for (int i = 0; i < count; i++)
                {
                    var password = _generator.Generate(length, options);
                    generatedList.Add(password);

                    dtoList.Add(new CreatePasswordDto
                    {
                        Title = "Generated Password",
                        UserName = "user",
                        Password = password
                    });
                }

                var isBatchSaved = await _apiClient.CreatePasswordsBatchAsync(dtoList);
                if (!isBatchSaved)
                {
                    System.Diagnostics.Debug.WriteLine("--> [ViewModel] Failed to sync generated passwords batch with server.");
                }

                _storage.SaveHistory(new PasswordHistoryData
                {
                    Options = options,
                    History = generatedList
                });

                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    _eventService.NotifyPasswordsGenerated(generatedList);
                });
            });
        }

        /// <summary>
        /// Загружает запись пароля с сервера по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи на сервере.</param>
        public async Task<PasswordDto?> LoadPasswordByIdAsync(int id)
        {
            return await _apiClient.GetPasswordByIdAsync(id);
        }

        /// <summary>
        /// Удаляет запись пароля на сервере по идентификатору и при успехе обновляет локальный список.
        /// </summary>
        /// <param name="id">Идентификатор записи на сервере.</param>
        /// <param name="passwordText">Значение пароля для удаления из <see cref="GeneratedPasswords"/>, если требуется.</param>
        public async Task<bool> DeletePasswordAsync(int id, string? passwordText = null)
        {
            var deleted = await _apiClient.DeletePasswordAsync(id);
            if (!deleted)
            {
                return false;
            }

            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                if (!string.IsNullOrEmpty(passwordText))
                {
                    GeneratedPasswords.Remove(passwordText);
                }
            });

            return true;
        }

        /// <summary>
        /// Обрабатывает событие генерации паролей: обновляет коллекцию для интерфейса.
        /// </summary>
        private void OnPasswordsGenerated(List<string> passwords)
        {
            GeneratedPasswords.Clear();
            foreach (var password in passwords)
            {
                GeneratedPasswords.Add(password);
            }
        }

        /// <summary>
        /// Копирует выбранный пароль в буфер обмена.
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
    }
}