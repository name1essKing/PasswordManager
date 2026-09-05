using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Реализация сервиса хранения паролей в формате JSON
    /// </summary>
    public class JsonPasswordStorageService : IPasswordStorageService
    {
        /// <summary>
        /// Путь файла
        /// </summary>
        private readonly string _filePath;

        public JsonPasswordStorageService()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var directoryPath = Path.Combine(appData, "PasswordManager");

            Directory.CreateDirectory(directoryPath);

            _filePath = Path.Combine(directoryPath, "password_history.json");
        }

        /// <inheritdoc/>
        public PasswordHistoryData LoadHistory()
        {
            if (!File.Exists(_filePath)) return default;

            try
            {
                string json = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(json)) return default;

                var data = JsonSerializer.Deserialize<PasswordHistoryData?>(json);

                return data ?? default;
            }
            catch
            {
                return default;
            }
        }

        /// <inheritdoc/>
        public void SaveHistory(PasswordHistoryData data)
        {
            var dataToSave = data.History == null
                ? data with { History = new List<string>() }
                : data;

            string json = JsonSerializer.Serialize(dataToSave);

            File.WriteAllText(_filePath, json);
        }
    }
}