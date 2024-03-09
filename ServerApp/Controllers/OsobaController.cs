using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Services;
using ServerApp.Services.OsobaServices;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OsobaController : Controller
    {
        private readonly OsobaService osobaService;
        public OsobaController(OsobaService service) { this.osobaService = service; }

        [HttpGet]
        [Route("all")]
        public ObjectResult GetAll()
        {
            return new ObjectResult(osobaService.GetAll());
        }
        [HttpPost]
        [Route("insert")]
        public ObjectResult Insert(Osoba osoba)
        {
            try
            {
                Osoba result = osobaService.Insert(osoba);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        /*[HttpPut]*/
        /*[Route("update")]
        public ObjectResult Update(Osoba osoba)
        {

        }*/
    }
}
