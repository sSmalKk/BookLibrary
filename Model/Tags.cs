public class Tag
{
    public int Codigo { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public ICollection<LivroTagPossui> Livros { get; set; } = new List<LivroTagPossui>();
}
