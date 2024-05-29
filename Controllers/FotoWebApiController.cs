using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FotoWebApiController : ControllerBase
    {
        [HttpGet]
        //("{name?}")
        public IActionResult GetFotos()
        {
            /*if (string.IsNullOrWhiteSpace())
            {
                return Ok(FotoManager.GetAllFoto());
            }*/


            return Ok(FotoManager.GetAllFoto());
        }

        [HttpGet("{id}")]
        public IActionResult GetFotoById(long id)
        {
            return Ok(FotoManager.MostraFoto(id));

            /*var foto = FotoManager.MostraFoto(id);
            if (foto == null)
            {
                return NotFound("Foto non trovata");
            }

            return Ok(foto);*/
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] Messaggio messaggio)
        {
            MessaggioManager.InviaMessaggio(messaggio);
            return Ok();
        }

    }
}
