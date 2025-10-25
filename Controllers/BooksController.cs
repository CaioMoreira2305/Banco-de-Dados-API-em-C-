using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;

namespace TrabalhoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db) => _db = db;

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _db.Books
                                  .Include(b => b.Categoria)
                                  .Select(b => new Book
                                  {
                                      Id = b.Id,
                                      Nome_Livro = b.Nome_Livro,
                                      Nome_Autor = b.Nome_Autor,
                                      Preco = b.Preco,
                                      Descricao = b.Descricao,
                                      Imagem_Url = b.Imagem_Url,
                                      Id_Categoria = b.Id_Categoria,
                                      CategoriaNome = b.Categoria != null ? b.Categoria.Categoria_Nome : string.Empty
                                  })
                                  .ToListAsync();
            return Ok(livros);
        }

        // GET: api/Books/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _db.Books
                                .Include(b => b.Categoria)
                                .Where(b => b.Id == id)
                                .Select(b => new Book
                                {
                                    Id = b.Id,
                                    Nome_Livro = b.Nome_Livro,
                                    Nome_Autor = b.Nome_Autor,
                                    Preco = b.Preco,
                                    Descricao = b.Descricao,
                                    Imagem_Url = b.Imagem_Url,
                                    Id_Categoria = b.Id_Categoria,
                                    CategoriaNome = b.Categoria != null ? b.Categoria.Categoria_Nome : string.Empty
                                })
                                .FirstOrDefaultAsync();

            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Books.Add(input);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        // PUT: api/Books/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.Nome_Livro = input.Nome_Livro;
            book.Nome_Autor = input.Nome_Autor;
            book.Preco = input.Preco;
            book.Descricao = input.Descricao;
            book.Imagem_Url = input.Imagem_Url;
            book.Id_Categoria = input.Id_Categoria;

            _db.Books.Update(book);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Books/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
