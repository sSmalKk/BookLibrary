using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryAPI.Model
{
    public class Livro
    {
        [Key]
        public int Codigo { get; set; } // Chave primária
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime Lancamento { get; set; }
        public ICollection<LivroTagPossui> LivroTagPossuis { get; set; }
    }

    public class LivroDigital : Livro
    {
        public string Formato { get; set; }
    }

    public class LivroImpresso : Livro
    {
        public double Peso { get; set; }
        public int TipoEncadernacaoCodigo { get; set; }
        [ForeignKey(nameof(TipoEncadernacaoCodigo))]
        public TipoEncadernacao TipoEncadernacao { get; set; }
        public ICollection<LivroImpressoTipoEncadernacaoPossui> LivroImpressoTipoEncadernacaoPossuis { get; set; }
    }

    public class TipoEncadernacao
    {
        [Key]
        public int Codigo { get; set; } // Chave primária
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Formato { get; set; }
        public ICollection<LivroImpresso> LivrosImpressos { get; set; }
        public ICollection<LivroImpressoTipoEncadernacaoPossui> LivroImpressoTipoEncadernacaoPossuis { get; set; }
    }

    public class Tag
    {
        [Key]
        public int Codigo { get; set; } // Chave primária
        public string Descricao { get; set; }
        public ICollection<LivroTagPossui> LivroTagPossuis { get; set; }
    }

    public class LivroTagPossui
    {
        [Key, Column(Order = 0)]
        public int LivroCodigo { get; set; }
        [ForeignKey(nameof(LivroCodigo))]
        public Livro Livro { get; set; }

        [Key, Column(Order = 1)]
        public int TagCodigo { get; set; }
        [ForeignKey(nameof(TagCodigo))]
        public Tag Tag { get; set; }
    }

    public class LivroImpressoTipoEncadernacaoPossui
    {
        [Key, Column(Order = 0)]
        public int LivroImpressoCodigo { get; set; }
        [ForeignKey(nameof(LivroImpressoCodigo))]
        public LivroImpresso LivroImpresso { get; set; }

        [Key, Column(Order = 1)]
        public int TipoEncadernacaoCodigo { get; set; }
        [ForeignKey(nameof(TipoEncadernacaoCodigo))]
        public TipoEncadernacao TipoEncadernacao { get; set; }
    }
}
