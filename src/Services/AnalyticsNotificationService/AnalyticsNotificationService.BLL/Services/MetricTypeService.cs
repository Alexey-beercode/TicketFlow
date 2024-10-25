using AnalyticsNotificationService.BLL.DTOs.Response.MetricType;
using AnalyticsNotificationService.BLL.Interfaces;
using AnalyticsNotificationService.DLL.Interfaces.Repositories;
using AnalyticsNotificationService.Domain.Entities;
using AutoMapper;
using BookingService.Application.Exceptions;

namespace AnalyticsNotificationService.BLL.Services;

public class MetricTypeService : IMetricTypeService
{
    private readonly IMetricTypeRepository _metricTypeRepository;
    private readonly IMapper _mapper;

    public MetricTypeService(IMetricTypeRepository metricTypeRepository, IMapper mapper)
    {
        _metricTypeRepository = metricTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MetricTypeResponseDto>> GetAllMetricTypesAsync()
    {
        var metricTypes = new List<MetricType>();
        await foreach (var metricType in _metricTypeRepository.GetAllAsync())
        {
            metricTypes.Add((MetricType)metricType);
        }

        return _mapper.Map<IEnumerable<MetricTypeResponseDto>>(metricTypes);
    }

    public async Task<MetricTypeResponseDto> GetMetricTypeByIdAsync(Guid id)
    {
        var metricType = await _metricTypeRepository.GetByIdAsync(id);

        if (metricType is null)
        {
            throw new EntityNotFoundException("MetricType", id);
        }

        return _mapper.Map<MetricTypeResponseDto>(metricType);
    }

    public async Task<MetricTypeResponseDto> GetMetricTypeByNameAsync(string name)
    {
        var metricType = await _metricTypeRepository.GetByNameAsync(name);

        if (metricType is null)
        {
            throw new EntityNotFoundException($"MetricType with name '{name}' not found");
        }

        return _mapper.Map<MetricTypeResponseDto>(metricType);
    }

    public async Task CreateMetricTypeAsync(string name)
    {
        var existingMetricType = await _metricTypeRepository.GetByNameAsync(name);

        if (existingMetricType is not null)
        {
            throw new AlreadyExistsException($"MetricType with name '{name}' already exists");
        }

        var metricType = new MetricType
        {
            Name = name,
        };

        await _metricTypeRepository.CreateAsync(metricType);
    }

    public async Task DeleteMetricTypeAsync(Guid id)
    {
        var metricType = await _metricTypeRepository.GetByIdAsync(id);

        if (metricType is null)
        {
            throw new EntityNotFoundException("MetricType", id);
        }

        await _metricTypeRepository.DeleteAsync(id);
    }
}