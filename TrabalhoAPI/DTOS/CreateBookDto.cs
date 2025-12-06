using System.ComponentModel.DataAnnotations;

namespace TrabalhoAPI.DTOs
{
    public class CreateBookDto
    {
        [Required, StringLength(200)]
        public string Nome_Livro { get; set; } = null!;

        [Required, StringLength(200)]
        public string Nome_Autor { get; set; } = null!;

        [StringLength(255)]
        public string? Imagem_Url { get; set; }

        public string? Descricao { get; set; }

        [Required]
        public int Id_Categoria { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal Preco { get; set; }
    }
}
