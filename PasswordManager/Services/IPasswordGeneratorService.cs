namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Интерфейс для сервиса генерации паролей.
    /// </summary>
    public interface IPasswordGeneratorService
    {
        /// <summary>
        /// Генерация пароля по заданным параметрам.
        /// </summary>
        /// <param name="length">Длина генерируемого пароля.</param>
        /// <param name="options">Опции генерации пароля.</param>
        /// <returns>Сгенерированный пароль.</returns>
        string Generate(int length, PasswordOptions options);
    }
}
