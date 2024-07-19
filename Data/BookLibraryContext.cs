using BookLibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

public class BookLibraryContext : DbContext
{
    public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
        : base(options)
    {
    }

    public DbSet<Livro> Livros { get; set; }
    public DbSet<LivroDigital> LivrosDigitais { get; set; }
    public DbSet<LivroImpresso> LivrosImpressos { get; set; }
    public DbSet<TipoEncadernacao> TiposEncadernacao { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<LivroTagPossui> LivroTagPossuis { get; set; }
    public DbSet<LivroImpressoTipoEncadernacaoPossui> LivroImpressoTipoEncadernacaoPossuis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração de Livro e suas subclasses
        modelBuilder.Entity<Livro>()
            .HasKey(l => l.Codigo);
        modelBuilder.Entity<Livro>()
            .HasDiscriminator<string>("Tipo")
            .HasValue<Livro>("Livro")
            .HasValue<LivroDigital>("LivroDigital")
            .HasValue<LivroImpresso>("LivroImpresso");

        // Configuração de LivroImpresso e TipoEncadernacao (1:N)
        modelBuilder.Entity<LivroImpresso>()
            .HasOne(li => li.TipoEncadernacao)
            .WithMany(te => te.LivrosImpressos)
            .HasForeignKey(li => li.TipoEncadernacaoCodigo)
            .IsRequired();

        // Configuração de Livro e Tag (N:N)
        modelBuilder.Entity<LivroTagPossui>()
            .HasKey(lt => new { lt.LivroCodigo, lt.TagCodigo });

        modelBuilder.Entity<LivroTagPossui>()
            .HasOne(lt => lt.Livro)
            .WithMany(l => l.LivroTagPossuis)
            .HasForeignKey(lt => lt.LivroCodigo);

        modelBuilder.Entity<LivroTagPossui>()
            .HasOne(lt => lt.Tag)
            .WithMany(t => t.LivroTagPossuis)
            .HasForeignKey(lt => lt.TagCodigo);

        // Configuração de LivroImpresso e TipoEncadernacaoPossui (N:N)
        modelBuilder.Entity<LivroImpressoTipoEncadernacaoPossui>()
            .HasKey(lie => new { lie.LivroImpressoId, lie.TipoEncadernacaoId });

        modelBuilder.Entity<LivroImpressoTipoEncadernacaoPossui>()
            .HasOne(lie => lie.LivroImpresso)
            .WithMany(li => li.LivroImpressoTipoEncadernacaoPossuis)
            .HasForeignKey(lie => lie.LivroImpressoId);

        modelBuilder.Entity<LivroImpressoTipoEncadernacaoPossui>()
            .HasOne(lie => lie.TipoEncadernacao)
            .WithMany(te => te.LivroImpressoTipoEncadernacaoPossuis)
            .HasForeignKey(lie => lie.TipoEncadernacaoId);

        // Configuração de Tag
        modelBuilder.Entity<Tag>()
            .HasKey(t => t.Codigo);

        // Configuração de TipoEncadernacao
        modelBuilder.Entity<TipoEncadernacao>()
            .HasKey(te => te.Codigo);

        base.OnModelCreating(modelBuilder);
    }
}

// Definição das entidades
public class Livro
{
    public int Codigo { get; set; }
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
    public int TipoEncadernacaoCodigo { get; set; }
    public TipoEncadernacao TipoEncadernacao { get; set; }
    public ICollection<LivroImpressoTipoEncadernacaoPossui> LivroImpressoTipoEncadernacaoPossuis { get; set; } = new HashSet<LivroImpressoTipoEncadernacaoPossui>();
}

public class TipoEncadernacao
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Formato { get; set; }
    public ICollection<LivroImpresso> LivrosImpressos { get; set; } = new HashSet<LivroImpresso>();
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
    public int LivroCodigo { get; set; }
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
