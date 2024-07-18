public class Livro
{
    public int Codigo { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public DateTime Lancamento { get; set; }
    public LivroDigital LivroDigital { get; set; }
    public LivroImpresso LivroImpresso { get; set; }
    public ICollection<LivroTagPossui> LivroTags { get; set; } = new List<LivroTagPossui>();
}
