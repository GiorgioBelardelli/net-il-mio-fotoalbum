using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace net_il_mio_fotoalbum.Models
{
    public class User : IdentityUser
    {
        public List<Foto> Fotos { get; set; }

        public User() { }
    }
}
