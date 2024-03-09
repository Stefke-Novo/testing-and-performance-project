using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class MestoController : Controller
    {
        [Route("/index")]
        [HttpGet]
        public ObjectResult Index()
        {
            return new ObjectResult(new List<string>() { "Mika", "Zika", "Pera" });
        }
    }
}
