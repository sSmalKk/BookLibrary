using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryAPI.Model
{
    public class Livro
    {
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
        public TipoEncadernacao TipoEncadernacao { get; set; }
    }

    public class TipoEncadernacao
    {
        public int Codigo { get; set; } // Chave primária
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Formato { get; set; }
        public ICollection<LivroImpresso> LivrosImpressos { get; set; }
    }

    public class Tag
    {
        public int Codigo { get; set; } // Chave primária
        public string Descricao { get; set; }
        public ICollection<LivroTagPossui> LivroTagPossuis { get; set; }
    }

    public class LivroTagPossui
    {
        public int LivroCodigo { get; set; }
        public Livro Livro { get; set; }
        public int TagCodigo { get; set; }
        public Tag Tag { get; set; }
    }

    public class LivroImpressoTipoEncadernacaoPossui
    {
        // Definição da relação entre LivroImpresso e TipoEncadernacao
    }

}
