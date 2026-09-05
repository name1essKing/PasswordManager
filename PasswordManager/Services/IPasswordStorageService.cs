

using System.Collections.ObjectModel;

namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Интерфейс для сервиса хранения истории паролей.
    /// </summary>
    public interface IPasswordStorageService
    {
        /// <summary>
        /// Загрузка истории при запуске приложения
        /// </summary>
        /// <returns>A PasswordHistoryData object containing the password history.</returns>
        PasswordHistoryData LoadHistory();

        /// <summary>
        /// Сохранение истории генерации паролей
        /// </summary>
        /// <param name="data">The PasswordHistoryData object to save.</param>
        void SaveHistory(PasswordHistoryData data);

    }
}
