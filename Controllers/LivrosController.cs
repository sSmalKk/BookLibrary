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
        try
        {
            // Validar valores de mês
            if (mes.HasValue && (mes < 1 || mes > 12))
            {
                return BadRequest("O mês deve estar entre 1 e 12.");
            }

            // Construir consulta
            var query = _context.Livros.AsQueryable();

            if (ano.HasValue)
            {
                query = query.Where(l => l.Lancamento.Year == ano.Value);
            }

            if (mes.HasValue)
            {
                query = query.Where(l => l.Lancamento.Month == mes.Value);
            }

            // Obter resultados
            var livros = await query.ToListAsync();
            return Ok(livros);
        }
        catch (Exception ex)
        {
            // Logar e retornar erro genérico
            // Logger.LogError(ex, "Erro ao buscar livros");
            return StatusCode(500, "Ocorreu um erro ao processar a solicitação.");
        }
    }
}

