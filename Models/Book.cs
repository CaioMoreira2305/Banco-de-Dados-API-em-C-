using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoAPI.Models
{
    [Table("tb_livros")]
    public class Book
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("nome_livro")]
        public string Nome_Livro { get; set; } = null!;

        [Required]
        [StringLength(200)]
        [Column("nome_autor")]
        public string Nome_Autor { get; set; } = null!;

        [StringLength(255)]
        [Column("imagem_url")]
        public string? Imagem_Url { get; set; }

        // Mapear para a coluna existente "descriao" do seu script
        [Column("descriao")]
        public string? Descricao { get; set; }

        [Required]
        [Column("id_categoria")]
        public int Id_Categoria { get; set; }

        [Column("preco", TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [NotMapped]
        public string CategoriaNome { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("Id_Categoria")]
        public Categoria? Categoria { get; set; }
    }
}
