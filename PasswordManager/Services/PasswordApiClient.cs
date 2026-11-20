using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PasswordManager.Dto.DTOs;

namespace PasswordManager.Client.Services;

/// <summary>
/// Сервис для взаимодействия с API сервера для управления паролями.
/// </summary>
public class PasswordApiClient : IPasswordApiClient
{
    private readonly HttpClient _httpClient;

    public PasswordApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<List<PasswordDto>> GetPasswordsAsync()
    {
        System.Diagnostics.Debug.WriteLine("--> [Client] GetPasswordsAsync called!");
        var response = await _httpClient.GetFromJsonAsync<List<PasswordDto>>("api/Passwords");
        return response ?? new List<PasswordDto>();
    }

    /// <inheritdoc/>
    public async Task<PasswordDto?> GetPasswordByIdAsync(int id)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Sending GET to: {_httpClient.BaseAddress}api/Passwords/{id}");
            var response = await _httpClient.GetAsync($"api/Passwords/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                System.Diagnostics.Debug.WriteLine($"--> [Client] Password with id={id} not found (404).");
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PasswordDto>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"--> [Client] Server error on GET by id: {response.StatusCode} - {errorContent}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Network request failed on GET by id: {ex.Message}");
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<PasswordDto?> CreatePasswordAsync(CreatePasswordDto dto)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Sending POST to: {_httpClient.BaseAddress}api/Passwords");
            var response = await _httpClient.PostAsJsonAsync("api/Passwords", dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PasswordDto>();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"--> [Client] Server error: {response.StatusCode} - {errorContent}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Network request failed: {ex.Message}");
        }

        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> CreatePasswordsBatchAsync(List<CreatePasswordDto> dtos)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Sending Batch POST (count: {dtos?.Count})");
            var response = await _httpClient.PostAsJsonAsync("api/Passwords/batch", dtos);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"--> [Client] Server batch error: {response.StatusCode} - {errorContent}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Batch network request failed: {ex.Message}");
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> DeletePasswordAsync(int id)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Sending DELETE to: {_httpClient.BaseAddress}api/Passwords/{id}");
            var response = await _httpClient.DeleteAsync($"api/Passwords/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"--> [Client] Server error on DELETE: {response.StatusCode} - {errorContent}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"--> [Client] Network request failed on DELETE: {ex.Message}");
        }

        return false;
    }
}