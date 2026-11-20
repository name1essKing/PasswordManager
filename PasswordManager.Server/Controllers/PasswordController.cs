using Microsoft.AspNetCore.Mvc;
using PasswordManager.Dto.DTOs;
using PasswordManager.Server.Services.Interfaces;

namespace PasswordManager.Server.Controllers;

/// <summary>
/// Контроллер для управления записями паролей через REST API.
/// </summary>
public class PasswordsController : PasswordControllerBase
{
    private readonly IPasswordsService _passwordsService;

    public PasswordsController(IPasswordsService passwordsService)
    {
        _passwordsService = passwordsService;
    }

    /// <summary>
    /// Возвращает список всех сохранённых паролей.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PasswordDto>>> GetAll(CancellationToken cancellationToken)
    {
        var dtos = await _passwordsService.GetAllAsync(cancellationToken);
        return Ok(dtos);
    }

    /// <summary>
    /// Возвращает запись пароля по указанному идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PasswordDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _passwordsService.GetByIdAsync(id, cancellationToken);
        if (dto == null)
        {
            return NotFound();
        }

        return Ok(dto);
    }

    /// <summary>
    /// Создаёт новую запись пароля.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PasswordDto>> Create([FromBody] CreatePasswordDto dto, CancellationToken cancellationToken)
    {
        var createdDto = await _passwordsService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdDto.Id }, createdDto);
    }

    /// <summary>
    /// Пакетно создаёт несколько записей паролей за одну транзакцию.
    /// </summary>
    [HttpPost("batch")]
    public async Task<IActionResult> CreateBatch([FromBody] List<CreatePasswordDto> dtos, CancellationToken cancellationToken)
    {
        var created = await _passwordsService.CreateBatchAsync(dtos, cancellationToken);
        if (!created)
        {
            return BadRequest();
        }

        return Ok();
    }

    /// <summary>
    /// Удаляет запись пароля по указанному идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _passwordsService.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}