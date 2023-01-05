using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Src.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            return Ok("Works !");
        }
    }
}
