using Microsoft.AspNetCore.Mvc;

namespace DrakeLambert.Peerra.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    { }
}