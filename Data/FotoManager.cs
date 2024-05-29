using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public class FotoManager
    {
        //METODO PER FARMI TORNARE LA LISTA DI FOTO
        public static List<Foto> GetAllFoto()
        {
            using FotoContext db = new FotoContext();
            return db.Foto.Include(f => f.Categorie).ToList();
        }

        //METODO PER FARMI TORNARE LE CATEGORIE
        public static List<Categorie> GetAllCategories()
        {
            using FotoContext db = new FotoContext();
            return db.Categorie.ToList();
        }

        //METODO PER TORNARE UNA SOLA FOTO
        public static Foto MostraFoto(long id, bool includeReferences = true)
        {
            using FotoContext db = new FotoContext();
            if (includeReferences)
                return db.Foto.Where(x => x.Id == id).Include(x => x.Categorie).FirstOrDefault();

            return db.Foto.FirstOrDefault(p => p.Id == id);
        }

        //METODO PER AGGIUNGERE UNA FOTO AL DATABASE
        public static void AggiungiFoto(Foto foto, List<string> categorieselezionate = null)
        {
            using FotoContext db = new FotoContext();
            foto.Categorie = new List<Categorie>();

            if (categorieselezionate != null)
            {
                foreach (var categoria in categorieselezionate)
                {
                    int id = int.Parse(categoria);
                    var categoriaDb = db.Categorie.FirstOrDefault(i => i.Id == id);

                    if (categoriaDb != null)
                    {
                        foto.Categorie.Add(categoriaDb);
                    }
                }
            }

            db.Foto.Add(foto);
            db.SaveChanges();
        }

        //METODO PER MODIFICARE UNA FOTO
        public static bool ModificaFoto(long id, string nome, string descrizione, byte[]? immagine, bool visibile, List<string> categorieselezionate)
        {
            try
            {
                using FotoContext db = new FotoContext();
                var fotoDaModificare = db.Foto.Where(p => p.Id == id).Include(p => p.Categorie).FirstOrDefault();

                if (fotoDaModificare == null)
                {
                    return false;
                }
                fotoDaModificare.Nome = nome;
                fotoDaModificare.Descrizione = descrizione;
                fotoDaModificare.Immagine = immagine;
                fotoDaModificare.Visibile = visibile;

                // Prima svuoto così da salvare solo le informazioni che l'utente ha scelto, NON le aggiungiamo ai vecchi dati
                fotoDaModificare.Categorie.Clear();

                if (categorieselezionate != null)
                {
                    foreach (var categoria in categorieselezionate)
                    {
                        int categoriaId = int.Parse(categoria);
                        var categoriaDb = db.Categorie.FirstOrDefault(x => x.Id == categoriaId);
                        if (categoriaDb != null)
                        {
                            fotoDaModificare.Categorie.Add(categoriaDb);
                        }
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //METODO PER ELIMINARE UNA FOTO
        public static bool EliminaFoto(long id)
        {
            using FotoContext db = new FotoContext();
            var fotoDaEliminare = db.Foto.FirstOrDefault(f => f.Id == id);

            if (fotoDaEliminare == null)
            {
                return false;
            }
            else
            {
                db.Foto.Remove(fotoDaEliminare);
                db.SaveChanges();
                return true;
            }
        }

        //CONTO LE PIZZE NEL DATABASE
        public static int ContaFoto()
        {
            using FotoContext db = new FotoContext();
            return db.Foto.Count();
        }

        //FUNZIONE PER INSERIRE LE FOTO
        public static void InserisciFoto(Foto foto)
        {
            using FotoContext db = new FotoContext();   
            db.Foto.Add(foto);
            db.SaveChanges();
        }


        //SEED PER INIZIALIZZARE IL DATABASE
        public static void Seed()
        {
            if (ContaFoto() == 0)
            {
                InserisciFoto(new Foto("Foto Seed 1", "Io e i miei amici nel seed", null, true));
                InserisciFoto(new Foto("Foto Seed 2", "Io e gli altri miei amici nel seed", null, true));
                InserisciFoto(new Foto("Foto Seed 3", "Io e gli amici che non ricordo nel seed", null, true));
            }
        }
    }
}
