using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cesarini.filippo._5H.SecondaWeb.Models
{
   public class PrenotazioniContext : DbContext
    {
        public DbSet<Prenotazione> Prenotazioni { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=database.db");
    }

}