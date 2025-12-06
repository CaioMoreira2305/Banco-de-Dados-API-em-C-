using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoAPI.Models
{
    [Table("tb_categorias")]
    public class Categoria
    {
        [Key]
        [Column("id_categoria")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Column("categoria_nome")]
        public string Nome { get; set; } = null!;
        
        // Navegação - IMPORTANTE: usar o nome correto da propriedade
        public virtual ICollection<Book>? Livros { get; set; }
    }
}