using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Data;

namespace net_il_mio_fotoalbum.Models
{
    public class FotoFormModel
    {
        public Foto Foto { get; set; }
        public User? User{ get; set; }
        public List<SelectListItem>? Categorie { get; set; }
        public List<string>? CategorieSelezionate { get; set; }

        public FotoFormModel() { }
        
        //Immagine
        public IFormFile? ImageFormFile { get; set; } 

        public FotoFormModel(Foto foto)
        {
            Foto = foto;

            CategorieSelezionate = new List<string>();
            if (Foto.Categorie != null)
                foreach (var photo in Foto.Categorie)
                    CategorieSelezionate.Add(photo.Id.ToString());
        }

        public void CreaCategorie()
        {
            this.Categorie = new List<SelectListItem>();
            if (this.CategorieSelezionate == null)
                this.CategorieSelezionate = new List<string>();
            var categorieDb = FotoManager.GetAllCategories();
            foreach (var categoria in categorieDb)
            {
                bool Selezionata = this.CategorieSelezionate.Contains(categoria.Id.ToString());
                this.Categorie.Add(new SelectListItem()
                {
                    Text = categoria.Nome,
                    Value = categoria.Id.ToString(),
                    Selected = Selezionata
                });
                if (Selezionata)
                { 
                    this.CategorieSelezionate.Add(categoria.Id.ToString()); 
                }
            }
        }

        public byte[] ImpostaImmagineForm()
        {
            if (ImageFormFile == null)
                return null;

            using var stream = new MemoryStream();
            this.ImageFormFile?.CopyTo(stream);
            Foto.Immagine = stream.ToArray();

            return Foto.Immagine;
        }
    }
}

