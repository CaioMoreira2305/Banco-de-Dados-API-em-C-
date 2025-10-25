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
        public int Id_Categoria { get; set; }

        [Required]
        [StringLength(100)]
        [Column("categoria_nome")]
        public string Categoria_Nome { get; set; } = null!;

        public ICollection<Book>? Livros { get; set; }
    }
}
