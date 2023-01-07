using Microsoft.AspNetCore.Mvc;
using Processing;

namespace API.Controllers
{
    public class ApplicationController : ControllerBase
    {
        protected ActionResult ToView(BaseResult result)
        {
            return result.State switch
            {
                OperationState.NotFound => NotFound(result),
                OperationState.BadRequest => BadRequest(result),
                OperationState.Undefined => StatusCode(500, result),
                OperationState.InternalError => StatusCode(500, result),
                _ => Ok(result)
            };
        }
    }
}
