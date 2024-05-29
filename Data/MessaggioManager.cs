using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public class MessaggioManager
    {
        public static void InviaMessaggio(Messaggio messaggio)
        {
            using FotoContext db = new FotoContext();
            db.Messaggi.Add(messaggio);
            db.SaveChanges();
        }
    }
}
