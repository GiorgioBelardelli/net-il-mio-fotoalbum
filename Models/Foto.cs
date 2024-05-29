using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    [Table("Photos")]
    public class Foto
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(30, ErrorMessage = "Massimo 30 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(100, ErrorMessage = "Massimo 100 caratteri")]
        public string Descrizione { get; set; }

        public byte[]? Immagine { get; set; }
        public string ImgSrc => Immagine != null ? $"data:image/jpeg;base64,{System.Convert.ToBase64String(Immagine)}" : "";
        public bool Visibile { get; set; }

        //Relazione categoria
        public List<Categorie>? Categorie { get; set; }



        //Costruttore vuoto per migrations
        public Foto() { }

        //Costruttore Foto
        public Foto(string nome, string descrizione, byte[] immagine, bool visibile)
        {
            Nome = nome;
            Descrizione = descrizione;
            Immagine = immagine;
            Visibile = visibile;
        }

    }
}
