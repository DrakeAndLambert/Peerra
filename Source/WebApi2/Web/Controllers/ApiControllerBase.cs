using Microsoft.AspNetCore.Mvc;

namespace DrakeLambert.Peerra.WebApi2.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    { }
}
