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

        // GET: api/Livros/{codigo}
        [HttpGet("{codigo}")]
        public ActionResult<Livro> GetLivroById(int codigo)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.Codigo == codigo); // Use 'Id' em vez de 'Codigo'

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

            return CreatedAtAction(nameof(GetLivroById), new { codigo = livro.Codigo }, livro); 
        }

        // PUT: api/Livros/{codigo}
        [HttpPut("{codigo}")]
        public IActionResult PutLivro(int codigo, Livro livro)
        {
            if (codigo != livro.Codigo)
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
                if (!_context.Livros.Any(e => e.Codigo == codigo))
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

        // DELETE: api/Livros/{codigo}
        [HttpDelete("{codigo}")]
        public IActionResult DeleteLivro(int codigo)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.Codigo == codigo);
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
                .FromSqlRaw("EXEC spLivros @Ano, @Mes",
                    new SqlParameter("@Ano", ano.HasValue ? (object)ano.Value : DBNull.Value),
                    new SqlParameter("@Mes", mes.HasValue ? (object)mes.Value : DBNull.Value))
                .ToListAsync();

            return Ok(livros);
        }
    }
}
