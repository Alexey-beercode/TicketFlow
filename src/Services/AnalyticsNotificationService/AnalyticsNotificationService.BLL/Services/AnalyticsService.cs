using AnalyticsNotificationService.BLL.DTOs.Response.Analytics;
using AnalyticsNotificationService.BLL.DTOs.Response.MetricType;
using AnalyticsNotificationService.BLL.Interfaces;
using AnalyticsNotificationService.DLL.Interfaces.Repositories;
using AnalyticsNotificationService.Domain.Entities;
using AutoMapper;
using BookingService.Application.Exceptions;

namespace AnalyticsNotificationService.BLL.Services;

public class AnalyticsService:IAnalyticsService
{
    private readonly IMetricTypeRepository _metricTypeRepository;
    private readonly IAnalyticsRepository _analyticsRepository;
    private readonly IMapper _mapper;

    public AnalyticsService(IMetricTypeRepository metricTypeRepository, IAnalyticsRepository analyticsRepository, IMapper mapper)
    {
        _metricTypeRepository = metricTypeRepository;
        _analyticsRepository = analyticsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnalyticsResponseDto>> GetAllAnalyticsAsync()
    {
        var analyticsList = new List<Analytics>();
        await foreach (var notification in _analyticsRepository.GetAllAsync())
        {
            analyticsList.Add(notification);
        }

        var analyticsResponseDtos = new List<AnalyticsResponseDto>();
        foreach (var analytics in analyticsList)
        {
            var metricType =
                _mapper.Map<MetricTypeResponseDto>(await _metricTypeRepository.GetByIdAsync(analytics.MetricTypeId));
            
            var analyticsResponseDto = _mapper.Map<AnalyticsResponseDto>(analytics);
            analyticsResponseDto.MetricType = metricType;
            
            analyticsResponseDtos.Add(analyticsResponseDto);
        }

        return analyticsResponseDtos;
    }

    public async Task<AnalyticsResponseDto> GetAnalyticsByIdAsync(Guid id)
    {
        var analytics = await _analyticsRepository.GetByIdAsync(id);

        if (analytics is null)
        {
            throw new EntityNotFoundException("Analytics", id);
        }

        var analyticsResponseDto = _mapper.Map<AnalyticsResponseDto>(analytics);

        var metricType =
            _mapper.Map<MetricTypeResponseDto>(await _metricTypeRepository.GetByIdAsync(analytics.MetricTypeId));
        analyticsResponseDto.MetricType = metricType;

        return analyticsResponseDto;
    }

    public async Task<IEnumerable<AnalyticsResponseDto>> GetAnalyticsByMetricTypeAsync(Guid metricTypeId)
    {
        var analyticsList = await _analyticsRepository.GetByMetricTypeIdAsync(metricTypeId);
        
        var analyticsResponseDtos = new List<AnalyticsResponseDto>();
        foreach (var analytics in analyticsList)
        {
            var metricType =
                _mapper.Map<MetricTypeResponseDto>(await _metricTypeRepository.GetByIdAsync(analytics.MetricTypeId));
            
            var analyticsResponseDto = _mapper.Map<AnalyticsResponseDto>(analytics);
            analyticsResponseDto.MetricType = metricType;
            
            analyticsResponseDtos.Add(analyticsResponseDto);
        }

        return analyticsResponseDtos;
    }

    public async Task DeleteAnalyticsAsync(Guid id)
    {
        var analytics = await _analyticsRepository.GetByIdAsync(id);

        if (analytics is null)
        {
            throw new EntityNotFoundException("Analytics", id);
        }

        await _analyticsRepository.DeleteAsync(id);
    }
}