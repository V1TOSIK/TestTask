using HotelBooking.Application.Dtos;
using MediatR;
using SharedKernel.Common;
using SharedKernel.Pagination;

namespace HotelBooking.Application.Hotel.Queries.GetHotels
{
    public class GetHotelsQuery : PaginationRequest, IRequest<Result<PaginationResponse<HotelDto>>>
    {
        public List<string>? Cities { get; set; }
    }
}
