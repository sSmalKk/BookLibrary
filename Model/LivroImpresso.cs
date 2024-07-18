public class LivroImpresso
{
    public int Codigo { get; set; }
    public decimal Peso { get; set; }
    public Livro Livro { get; set; }
    public ICollection<LivroImpressoTipoEncadernacaoPossui> TipoEncadernacoes { get; set; } = new List<LivroImpressoTipoEncadernacaoPossui>();
}
