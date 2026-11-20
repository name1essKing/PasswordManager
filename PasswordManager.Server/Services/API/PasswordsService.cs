using AutoMapper;
using PasswordManager.Dto.DTOs;
using PasswordManager.Server.Models;
using PasswordManager.Server.Services.API.Interfaces;
using PasswordManager.Server.Services.Interfaces;
using PasswordManager.Server.UnitOfWork;

namespace PasswordManager.Server.Services;

/// <summary>
/// Сервис управления записями паролей, координирующий репозиторий, Unit of Work и маппинг.
/// </summary>
public class PasswordsService : IPasswordsService
{
    private readonly IPasswordsRepository _passwordsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PasswordsService(
        IPasswordsRepository passwordsRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _passwordsRepository = passwordsRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<PasswordDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _passwordsRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<PasswordDto>>(items);
    }

    /// <inheritdoc />
    public async Task<PasswordDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _passwordsRepository.GetByIdAsync(id, cancellationToken);
        return item == null ? null : _mapper.Map<PasswordDto>(item);
    }

    /// <inheritdoc />
    public async Task<PasswordDto> CreateAsync(CreatePasswordDto dto, CancellationToken cancellationToken = default)
    {
        var item = _mapper.Map<PasswordItem>(dto);

        await _passwordsRepository.AddAsync(item, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PasswordDto>(item);
    }

    /// <inheritdoc />
    public async Task<bool> CreateBatchAsync(IReadOnlyCollection<CreatePasswordDto> dtos, CancellationToken cancellationToken = default)
    {
        if (dtos == null || dtos.Count == 0)
        {
            return false;
        }

        var items = _mapper.Map<List<PasswordItem>>(dtos);

        await _passwordsRepository.AddRangeAsync(items, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _passwordsRepository.GetByIdAsync(id, cancellationToken);
        if (item == null)
        {
            return false;
        }

        _passwordsRepository.Delete(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}