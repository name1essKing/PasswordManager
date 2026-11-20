namespace PasswordManager.Client.Services
{
    public interface IPasswordSettingsService
    {
        /// <summary>
        /// длина генерируемого пароля.
        /// </summary>
        int PasswordLength { get; set; }
        /// <summary>
        /// количество генерируемых паролей.
        /// </summary>
        int PasswordsCount { get; set; }
        /// <summary>
        /// использовать ли цифры в пароле.
        /// </summary>
        bool UseDigit { get; set; }
        /// <summary>
        /// использовать ли символы в пароле.
        /// </summary>
        bool UseSymbols { get; set; }
        /// <summary>
        /// использовать ли заглавные буквы в пароле.
        /// </summary>
        bool UseUpperCase { get; set; }
    }
}