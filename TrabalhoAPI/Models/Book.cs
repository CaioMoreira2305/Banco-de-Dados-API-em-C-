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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do livro é obrigatório")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Column("nome_livro")]
        public string Nome_Livro { get; set; } = null!;

        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Column("nome_autor")]
        public string Nome_Autor { get; set; } = null!;

        [StringLength(255)]
        [Column("imagem_url")]
        public string? Imagem_Url { get; set; }

        // ATENÇÃO: Mantém o nome errado "descriao" igual ao banco
        [Column("descriao")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [Column("id_categoria")]
        public int Id_Categoria { get; set; }

        [Column("preco", TypeName = "decimal(10,2)")]
        [Range(0.00, 9999.99, ErrorMessage = "Preço deve ser entre 0.00 e 9999.99")]
        public decimal Preco { get; set; } = 0.00m;

        // Navigation property
        [ForeignKey("Id_Categoria")]
        public virtual Categoria? Categoria { get; set; }

        // NotMapped para uso na GUI
        [NotMapped]
        public string CategoriaNome { get; set; } = string.Empty;
    }
}