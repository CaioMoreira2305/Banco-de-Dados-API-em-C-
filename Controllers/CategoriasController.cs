using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;

namespace TrabalhoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CategoriasController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _db.Categorias.OrderBy(c => c.Categoria_Nome).ToListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _db.Categorias.FindAsync(id);
            if (cat == null) return NotFound(new { message = "Categoria não encontrada" });
            return Ok(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Categoria input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Categorias.Add(input);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = input.Id_Categoria }, input);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var cat = await _db.Categorias.FindAsync(id);
            if (cat == null) return NotFound();
            cat.Categoria_Nome = input.Categoria_Nome;
            _db.Categorias.Update(cat);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _db.Categorias.FindAsync(id);
            if (cat == null) return NotFound();
            // opcional: checar se tem livros vinculados
            bool hasBooks = await _db.Books.AnyAsync(b => b.Id_Categoria == id);
            if (hasBooks) return Conflict(new { message = "Categoria possui livros vinculados" });
            _db.Categorias.Remove(cat);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
