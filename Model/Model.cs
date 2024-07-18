using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Model
{
    public class TipoEncadernacao
    {
        public int Codigo { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Descricao { get; set; } = string.Empty;
        [Required]
        public string Formato { get; set; } = string.Empty;
        public ICollection<LivroImpressoTipoEncadernacaoPossui> LivroImpressos { get; set; } = new List<LivroImpressoTipoEncadernacaoPossui>();
    }
    public class Tag
    {
        public int Codigo { get; set; }
        [Required]
        public string Descricao { get; set; } = string.Empty;
        public ICollection<LivroTagPossui> Livros { get; set; } = new List<LivroTagPossui>();
    }
    public class LivroTagPossui
    {
        public Livro Livro { get; set; }
        public Tag Tag { get; set; }
    }

    public class LivroImpressoTipoEncadernacaoPossui
    {
        public LivroImpresso LivroImpresso { get; set; }
        public TipoEncadernacao TipoEncadernacao { get; set; }
    }
    public class LivroImpresso
    {
        public int Codigo { get; set; }
        [Required]
        public decimal Peso { get; set; }
        public Livro Livro { get; set; }
        public ICollection<LivroImpressoTipoEncadernacaoPossui> TipoEncadernacoes { get; set; } = new List<LivroImpressoTipoEncadernacaoPossui>();
    }
    public class LivroDigital
    {
        public int Codigo { get; set; }
        [Required]
        public string Formato { get; set; } = string.Empty;
        public Livro Livro { get; set; }
    }
    public class Livro
    {
        public int Codigo { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        public string Autor { get; set; } = string.Empty;
        [Required]
        public DateTime Lancamento { get; set; }
        public LivroDigital LivroDigital { get; set; }
        public LivroImpresso LivroImpresso { get; set; }
        public ICollection<LivroTagPossui> LivroTags { get; set; } = new List<LivroTagPossui>();
    }
}