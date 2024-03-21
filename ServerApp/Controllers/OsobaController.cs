using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Services;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OsobaController : Controller
    {
        private readonly OsobaService osobaService;
        public OsobaController(OsobaService service) { this.osobaService = service; }

        /*[HttpGet]
        [Route("all")]
        public ObjectResult GetAll()
        {
            return new ObjectResult(osobaService.GetAll());
        }*/
        [HttpPost]
        [Route("insert")]
        public ObjectResult Insert(Osoba osoba)
        {
            try
            {
                Osoba result = osobaService.Insert(osoba);
                return Ok(result);
            }
            catch (Exception)
            { //return BadRequest(ex.Message); }
                throw;
            }
        }
        /*[HttpPut]
        [Route("update")]
        public ObjectResult Update(Osoba osoba)
        {
            try
            {
                return Ok(osobaService.Update(osoba));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("delete")]
        public ObjectResult Delete(Osoba osoba)
        {
            try
            {
                return Ok(osobaService.Delete(osoba));
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
