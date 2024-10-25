using AutoMapper;
using BookingService.Application.DTOs.Response.DiscountType;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.DIscountType.GetAll;

public class GetAllDiscountTypesHandler:IRequestHandler<GetAllDiscountTypesQuery,IEnumerable<DiscountTypeResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDiscountTypesHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<DiscountTypeResponseDto>> Handle(GetAllDiscountTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _unitOfWork.DiscountTypes.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<DiscountTypeResponseDto>>(types);
    }
}