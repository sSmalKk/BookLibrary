using Microsoft.AspNetCore.Mvc;
using BookLibraryAPI.Data;
using BookLibraryAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace BookLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly BookLibraryContext _context;

        public LivrosController(BookLibraryContext context)
        {
            _context = context;
        }

        // Método GET existente para obter todos os livros
        [HttpGet]
        public ActionResult<IEnumerable<Livro>> Get()
        {
            return _context.Livros.ToList();
        }

        // Novo método GET para obter um livro por ID
        [HttpGet("{id}")]
        public ActionResult<Livro> GetById(int id)
        {
            var livro = _context.Livros.Find(id);
            if (livro == null)
            {
                return NotFound();
            }
            return livro;
        }

        // Novo método POST para adicionar um novo livro
        [HttpPost]
        public ActionResult<Livro> Create(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = livro.Codigo }, livro);
        }

        // Novo método PUT para atualizar um livro existente
        [HttpPut("{id}")]
        public IActionResult Update(int id, Livro livro)
        {
            if (id != livro.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
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

        // Novo método DELETE para excluir um livro
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var livro = _context.Livros.Find(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            _context.SaveChanges();

            return NoContent();
        }

        // Outros métodos do controlador...
    }
}
