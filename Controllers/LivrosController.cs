using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookLibraryAPI.Model; // Certifique-se de que este é o namespace correto
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BookLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly BookLibraryContext _context;

        public LivrosController(BookLibraryContext context)
        {
            _context = context;
        }

        // GET: api/Livros/{id}
        [HttpGet("{id}")]
        public ActionResult<Livro> GetLivroById(int id)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.Codigo == id); // Use 'Id' em vez de 'Codigo'

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        // POST: api/Livros
        [HttpPost]
        public ActionResult<Livro> PostLivro(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLivroById), new { id = livro.Codigo }, livro); // Use 'Id' em vez de 'Codigo'
        }

        // PUT: api/Livros/{id}
        [HttpPut("{id}")]
        public IActionResult PutLivro(int id, Livro livro)
        {
            if (id != livro.Codigo) // Ajustado para usar `Id` conforme definido
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Livros.Any(e => e.Codigo == id))
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

        // DELETE: api/Livros/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteLivro(int id)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.Codigo == id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            _context.SaveChanges();

            return NoContent();
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<IActionResult> GetLivros([FromQuery] int? ano = null, [FromQuery] int? mes = null)
        {
            var livros = await _context.Livros
                .FromSqlRaw("EXEC spListarLivrosComFiltro @Ano, @Mes",
                    new SqlParameter("@Ano", ano.HasValue ? (object)ano.Value : DBNull.Value),
                    new SqlParameter("@Mes", mes.HasValue ? (object)mes.Value : DBNull.Value))
                .ToListAsync();

            return Ok(livros);
        }
    }
}
