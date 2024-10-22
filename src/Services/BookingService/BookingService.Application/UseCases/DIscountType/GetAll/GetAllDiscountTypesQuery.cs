using BookingService.Application.DTOs.Response.DiscountType;
using MediatR;

namespace BookingService.Application.UseCases.DIscountType.GetAll;

public class GetAllDiscountTypesQuery:IRequest<IEnumerable<DiscountTypeResponseDto>>
{
    
}