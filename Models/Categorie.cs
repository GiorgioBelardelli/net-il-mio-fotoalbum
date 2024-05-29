using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Categorie
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Nome { get; set; }

        //Relazione Foto
        public List<Foto>? Foto { get; set; }


        //costruttore vuto per migrations
        public Categorie() { }
    }
}
