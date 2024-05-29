using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class FotoController : Controller
    {
        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Index()
        {
            List<Foto> foto = FotoManager.GetAllFoto();
            return View("Index", foto);
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult Show(long id)
        {

            try
            {
                var foto = FotoManager.MostraFoto(id);
                if (foto != null)
                    return View(foto);
                else
                    return NotFound();
                //return View("Errore", new ErroreViewModel($"La pizza {id} non è stata trovata!"));
            }
            catch (Exception e)
            {
                //return View("Errore", new ErroreViewModel(e.Message));
                return BadRequest(e.Message);
            }

        }


        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            FotoFormModel model = new FotoFormModel();
            model.Foto = new Foto();
            model.CreaCategorie();

            return View(model);
        }

        //Action che fornisce la view con la form per creare una pizza
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create(FotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreaCategorie();
                return View("Create", data);
            }
            data.ImpostaImmagineForm();
            FotoManager.AggiungiFoto(data.Foto, data.CategorieSelezionate);
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id)
        {
            var fotoDaModificare = FotoManager.MostraFoto(id);

            if (fotoDaModificare == null)
            {
                return NotFound();
            }
            else
            {
                FotoFormModel model = new FotoFormModel();
                model.Foto = fotoDaModificare;
                model.CreaCategorie();

                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(long id, FotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreaCategorie();
                return View("Update", data);
            }

            data.ImpostaImmagineForm();

            if (FotoManager.ModificaFoto(id, data.Foto.Nome, data.Foto.Descrizione, data.Foto.Immagine, data.Foto.Visibile, data.CategorieSelezionate))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            if (FotoManager.EliminaFoto(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }

        }
    }
}
