using PasswordManager.Client.Services;
using PasswordManager.ui;
using ReactiveUI;
namespace PasswordManager.Client.Views
{
    public sealed partial class PasswordOptionsViewModel : ViewModelBase
    {
        // Сервисы
        private readonly IPasswordGeneratorService _generator;
        private readonly IPasswordStorageService _storage;
        private readonly IPasswordEventService _eventService;

        // Поля настроек генератора
        private int _passwordLength;
        private bool _useDigit;
        private bool _useSymbols;
        private bool _useUpperCase;
        private int _passwordsCount;

        /// <summary>
        /// Длина пароля
        /// </summary>
        public int PasswordLength
        {
            get => _passwordLength;
            set => this.RaiseAndSetIfChanged(ref _passwordLength, value);
        }

        /// <summary>
        /// Использовать цифры?
        /// </summary>
        public bool UseDigit
        {
            get => _useDigit;
            set => this.RaiseAndSetIfChanged(ref _useDigit, value);
        }

        /// <summary>
        /// Использовать спецсимволы?
        /// </summary>
        public bool UseSymbols
        {
            get => _useSymbols;
            set => this.RaiseAndSetIfChanged(ref _useSymbols, value);
        }

        /// <summary>
        /// Использовать заглавные буквы?
        /// </summary>
        public bool UseUpperCase
        {
            get => _useUpperCase;
            set => this.RaiseAndSetIfChanged(ref _useUpperCase, value);
        }

        /// <summary>
        /// Количество генерируемых паролей за раз
        /// </summary>
        public int PasswordsCount
        {
            get => _passwordsCount;
            set => this.RaiseAndSetIfChanged(ref _passwordsCount, value);
        }

        public PasswordOptionsViewModel(IPasswordGeneratorService generator, IPasswordStorageService storage, IPasswordEventService eventService)
        {
            _generator = generator;
            _storage = storage;
            _eventService = eventService;

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
        /// Генерация новых паролей и перезапись сохраненного результата последней сессии в JSON
        /// </summary>
        public void GeneratePassword()
        {
            var options = PasswordOptions.None;
            if (UseDigit) options |= PasswordOptions.Digits;
            if (UseSymbols) options |= PasswordOptions.Symbols;
            if (UseUpperCase) options |= PasswordOptions.UpperCase;

            var generatedList = new List<string>();

            for (int i = 0; i < PasswordsCount; i++)
            {
                var password = _generator.Generate(PasswordLength, options);
                generatedList.Add(password);
            }

            _storage.SaveHistory(new PasswordHistoryData
            {
                Options = options,
                History = generatedList
            });

            _eventService.NotifyPasswordsGenerated(generatedList);
        }
    }
}