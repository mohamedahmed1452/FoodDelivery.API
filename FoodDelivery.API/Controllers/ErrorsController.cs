using FoodDelivery.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ErrorsController : ControllerBase
    {

        [HttpGet]
        public ActionResult<ApiResponse> Get(int code)
        {
            return code switch
            {
                400 => new ApiResponse(400),
                401 => new ApiResponse(401),
                403 => new ApiResponse(403),
                404 => new ApiResponse(404),
                500 => new ApiResponse(500),
                _ => new ApiResponse(409)
            };
        }
    }
}
