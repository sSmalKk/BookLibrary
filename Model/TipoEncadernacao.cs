public class TipoEncadernacao
{
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Formato { get; set; } = string.Empty;
    public ICollection<LivroImpressoTipoEncadernacaoPossui> LivroImpressos { get; set; } = new List<LivroImpressoTipoEncadernacaoPossui>();
}
