using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Models;

namespace TrabalhoAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Book> Books { get; set; } // <-- Agora você pode usar _db.Books

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação Book -> Categoria
            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Categoria)
                        .WithMany(c => c.Livros)
                        .HasForeignKey(b => b.Id_Categoria)
                        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
