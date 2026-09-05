using System.Collections.Generic;

namespace PasswordManager.Client.Services
{
    /// <summary>
    /// Представляет данные истории паролей, включая опции генерации и список сгенерированных паролей.
    /// </summary>
    /// <param name="Options"></param>
    /// <param name="History"></param>
    public readonly record struct PasswordHistoryData(PasswordOptions Options, List<string> History);
}