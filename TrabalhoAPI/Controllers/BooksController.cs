using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabalhoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBooks()
        {
            // INNER JOIN com tb_categorias
            var livros = await _context.Books
                .Include(b => b.Categoria)
                .Select(b => new
                {
                    b.Id,
                    b.Nome_Livro,
                    b.Nome_Autor,
                    b.Imagem_Url,
                    b.Descricao,
                    b.Preco,
                    b.Id_Categoria,
                    CategoriaNome = b.Categoria != null ? b.Categoria.Nome : "Sem Categoria"
                })
                .ToListAsync();
            
            return Ok(livros);
        }

        // GET: api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Categoria)
                .Where(b => b.Id == id)
                .Select(b => new
                {
                    b.Id,
                    b.Nome_Livro,
                    b.Nome_Autor,
                    b.Imagem_Url,
                    b.Descricao,
                    b.Preco,
                    b.Id_Categoria,
                    CategoriaNome = b.Categoria != null ? b.Categoria.Nome : "Sem Categoria"
                })
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound(new { message = "Livro não encontrado" });
            }

            return book;
        }

        // GET: api/books/search?nome=harry
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchBooks([FromQuery] string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return await GetBooks();
            }

            var livros = await _context.Books
                .Include(b => b.Categoria)
                .Where(b => b.Nome_Livro.Contains(nome) || b.Nome_Autor.Contains(nome))
                .Select(b => new
                {
                    b.Id,
                    b.Nome_Livro,
                    b.Nome_Autor,
                    b.Imagem_Url,
                    b.Descricao,
                    b.Preco,
                    b.Id_Categoria,
                    CategoriaNome = b.Categoria != null ? b.Categoria.Nome : "Sem Categoria"
                })
                .ToListAsync();

            return livros;
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            // Verificar se a categoria existe
            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == book.Id_Categoria);
            
            if (!categoriaExiste)
            {
                return BadRequest(new { message = "Categoria não encontrada" });
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest(new { message = "IDs não correspondem" });
            }

            // Verificar se a categoria existe
            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == book.Id_Categoria);
            
            if (!categoriaExiste)
            {
                return BadRequest(new { message = "Categoria não encontrada" });
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}