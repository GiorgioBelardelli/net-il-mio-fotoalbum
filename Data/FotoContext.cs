using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data

{
    public class FotoContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Messaggio> Messaggi { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-foto;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
