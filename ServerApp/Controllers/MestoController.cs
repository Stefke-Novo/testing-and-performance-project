using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Services;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class MestoController : Controller
    {
        private readonly MestoService mestoService;
        public MestoController(MestoService mestoService)
        {
            this.mestoService = mestoService;
        }
        [Route("all")]
        [HttpGet]
        public ObjectResult Index()
        {
            return Ok(this.mestoService.GetAll());
        }
    }
}
