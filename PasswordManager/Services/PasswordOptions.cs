namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Возможные опции для генерации пароля.
    /// </summary>
    [Flags]
    public enum PasswordOptions
    {
        None = 0,
        Digits = 1,
        Symbols = 2,
        UpperCase = 4
    }
}
