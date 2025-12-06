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
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuração da Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("tb_categorias");
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                
                // Relação um-para-muitos
                entity.HasMany(e => e.Livros)
                      .WithOne(e => e.Categoria)
                      .HasForeignKey(e => e.Id_Categoria)
                      .HasConstraintName("fk_categorias")
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração do Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("tb_livros");
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                
                // Propriedades
                entity.Property(e => e.Nome_Livro)
                      .HasColumnName("nome_livro")
                      .IsRequired()
                      .HasMaxLength(200);
                
                entity.Property(e => e.Nome_Autor)
                      .HasColumnName("nome_autor")
                      .IsRequired()
                      .HasMaxLength(200);
                
                entity.Property(e => e.Imagem_Url)
                      .HasColumnName("imagem_url")
                      .HasMaxLength(255);
                
                entity.Property(e => e.Descricao)
                      .HasColumnName("descriao"); // Nome com typo
                
                entity.Property(e => e.Preco)
                      .HasColumnName("preco")
                      .HasColumnType("decimal(10,2)")
                      .HasDefaultValue(0.00m);
                
                entity.Property(e => e.Id_Categoria)
                      .HasColumnName("id_categoria")
                      .IsRequired();
            });
        }
    }
}