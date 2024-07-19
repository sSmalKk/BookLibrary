using System;
using System.Collections.Generic;

namespace BookLibraryAPI.Model
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime Lancamento { get; set; }
        public ICollection<LivroTagPossui> LivroTagPossuis { get; set; } = new HashSet<LivroTagPossui>();
    }

    public class LivroDigital : Livro
    {
        public string Formato { get; set; }
    }

    public class LivroImpresso : Livro
    {
        public double Peso { get; set; }
        public ICollection<LivroImpressoTipoEncadernacaoPossui> LivroImpressoTipoEncadernacaoPossuis { get; set; } = new HashSet<LivroImpressoTipoEncadernacaoPossui>();
    }

    public class TipoEncadernacao
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Formato { get; set; }
        public ICollection<LivroImpressoTipoEncadernacaoPossui> LivroImpressoTipoEncadernacaoPossuis { get; set; } = new HashSet<LivroImpressoTipoEncadernacaoPossui>();
    }

    public class Tag
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public ICollection<LivroTagPossui> LivroTagPossuis { get; set; } = new HashSet<LivroTagPossui>();
    }

    public class LivroTagPossui
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int TagCodigo { get; set; }
        public Tag Tag { get; set; }
    }

    public class LivroImpressoTipoEncadernacaoPossui
    {
        public int LivroImpressoId { get; set; }
        public LivroImpresso LivroImpresso { get; set; }
        public int TipoEncadernacaoId { get; set; }
        public TipoEncadernacao TipoEncadernacao { get; set; }
    }
}
