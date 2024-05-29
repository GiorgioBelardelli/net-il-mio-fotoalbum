using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    [Table("Messages")]
    public class Messaggio
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(30, ErrorMessage = "Massimo 30 caratteri")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(100, ErrorMessage = "Massimo 100 caratteri")]
        public string Testo { get; set; }

        public DateTime Data { get; set; } = DateTime.Now;

        public Messaggio() { }

        public Messaggio (string email, string testo)
        { 
            Email = email;
            Testo = testo;
        }
    }
}
