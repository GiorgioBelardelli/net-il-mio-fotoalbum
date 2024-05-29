using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Authorization;


namespace net_il_mio_fotoalbum.Controllers
{
    public class CategoriaController : Controller
    {
        [Authorize(Roles = "ADMIN, USER")]
        public IActionResult Index()
        {
            return View(FotoManager.GetAllCategories());
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            Categorie categoria = new Categorie();

            return View(categoria);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create(Categorie categoria)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categoria);
            }
            CategoriaManager.AggiungiCategoria(categoria);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(long id)
        {
            if (CategoriaManager.EliminaCategoria(id))
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
