using BookLibraryAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly BookLibraryContext _context;

    public LivrosController(BookLibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Livro>>> GetLivros([FromQuery] int? ano, [FromQuery] int? mes)
    {
        var query = _context.Livros.AsQueryable();

        if (ano.HasValue)
        {
            query = query.Where(l => l.Lancamento.Year == ano.Value);
            if (mes.HasValue)
            {
                query = query.Where(l => l.Lancamento.Month == mes.Value);
            }
        }

        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Livro>> GetLivro(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null)
        {
            return NotFound();
        }

        return livro;
    }

    [HttpPost]
    public async Task<ActionResult<Livro>> PostLivro(Livro livro)
    {
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLivro), new { id = livro.Codigo }, livro);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutLivro(int id, Livro livro)
    {
        if (id != livro.Codigo)
        {
            return BadRequest();
        }

        _context.Entry(livro).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LivroExists(id))
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

    private bool LivroExists(int id)
    {
        return _context.Livros.Any(e => e.Codigo == id);
    }
}
