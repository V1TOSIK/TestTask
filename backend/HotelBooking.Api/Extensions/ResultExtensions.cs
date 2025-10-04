using Microsoft.AspNetCore.Mvc;
using SharedKernel.Common;

namespace HotelBooking.Api.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.Ok(result);
            return controller.BadRequest(result);
        }
    }
}
