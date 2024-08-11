using Microsoft.EntityFrameworkCore;

namespace AspWebApi.Data.Models
{
    public class ApiDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Personne> People { get; set; }
        
        /// <summary>
        /// on décrit a EF core comment l'objet person doit etre mis dans la base de donnée
        /// </summary>
        public ApiDbContext ( DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personne>(c =>
            {
                c.ToTable("Personne");
                c.Property(p => p.Name).HasMaxLength(256);
                c.Property(p => p.LastName).HasMaxLength(256);
                //c.Ignore(p => p.Birthday);
            });
            
        }
        /// <summary>
        /// on a crée la chaine de connexion 
        /// </summary>
         
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlite("filename=api.db");
           
    //    }
    }
}
