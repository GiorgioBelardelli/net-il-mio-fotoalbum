using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public class CategoriaManager
    {

        public static void AggiungiCategoria(Categorie categoria)
        {
            using FotoContext db = new FotoContext();
            db.Categorie.Add(categoria);
            db.SaveChanges();
        }

        public static bool EliminaCategoria(long id)
        {
            using FotoContext db = new FotoContext();
            var categoriaDaEliminare = db.Categorie.FirstOrDefault(c => c.Id == id);

            if (categoriaDaEliminare == null)
            {
                return false;
            }
            else
            {
                db.Categorie.Remove(categoriaDaEliminare);
                db.SaveChanges();
                return true;
            }
        }
    }
}
