using System;
using System.Text;

namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Реализация сервиса генерации паролей.
    /// </summary>
    public class PasswordGeneratorService : IPasswordGeneratorService
    {
        // Базовые наборы символов
        private const string LowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string DigitChars = "0123456789";
        private const string SymbolChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        /// <inheritdoc/>
        public string Generate(int length, PasswordOptions options)
        {
            var charSet = LowerCaseChars;

            if (options.HasFlag(PasswordOptions.UpperCase))
                charSet += LowerCaseChars.ToUpper();

            if (options.HasFlag(PasswordOptions.Digits))
                charSet += DigitChars;

            if (options.HasFlag(PasswordOptions.Symbols))
                charSet += SymbolChars;

            if (string.IsNullOrEmpty(charSet) || length < 4)
                return string.Empty;

            var random = new Random();
            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                char randomChar = charSet[random.Next(charSet.Length)];
                result.Append(randomChar);
            }

            return result.ToString();
        }
    }
}