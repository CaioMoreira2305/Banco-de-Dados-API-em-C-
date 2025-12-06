using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrabalhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // GET: api/categorias/5/livros (livros por categoria)
        [HttpGet("{id}/livros")]
        public async Task<ActionResult<IEnumerable<Book>>> GetLivrosPorCategoria(int id)
        {
            var livros = await _context.Books
                .Include(b => b.Categoria)
                .Where(b => b.Id_Categoria == id)
                .ToListAsync();

            return livros;
        }
    }
}