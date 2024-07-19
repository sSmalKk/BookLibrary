using Microsoft.EntityFrameworkCore;
using BookLibraryAPI.Model;

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
            .HasKey(l => l.Codigo); // Chave primária

        // Configuração de LivroDigital
        modelBuilder.Entity<LivroDigital>()
            .HasBaseType<Livro>();

        // Configuração de LivroImpresso
        modelBuilder.Entity<LivroImpresso>()
            .HasBaseType<Livro>();

        // Configuração de LivroImpresso e TipoEncadernacao (1:N)
        modelBuilder.Entity<LivroImpresso>()
            .HasOne(li => li.TipoEncadernacao)
            .WithMany(te => te.LivrosImpressos)
            .HasForeignKey(li => li.TipoEncadernacaoCodigo)
            .IsRequired();

        // Configuração de Livro e Tag (N:N)
        modelBuilder.Entity<LivroTagPossui>()
            .HasKey(lt => new { lt.LivroCodigo, lt.TagCodigo }); // Chave composta

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
            .HasKey(lie => new { lie.LivroImpressoCodigo, lie.TipoEncadernacaoCodigo }); // Chave composta

        modelBuilder.Entity<LivroImpressoTipoEncadernacaoPossui>()
            .HasOne(lie => lie.LivroImpresso)
            .WithMany(li => li.LivroImpressoTipoEncadernacaoPossuis)
            .HasForeignKey(lie => lie.LivroImpressoCodigo);

        modelBuilder.Entity<LivroImpressoTipoEncadernacaoPossui>()
            .HasOne(lie => lie.TipoEncadernacao)
            .WithMany(te => te.LivroImpressoTipoEncadernacaoPossuis)
            .HasForeignKey(lie => lie.TipoEncadernacaoCodigo);

        // Configuração de Tag
        modelBuilder.Entity<Tag>()
            .HasKey(t => t.Codigo);

        // Configuração de TipoEncadernacao
        modelBuilder.Entity<TipoEncadernacao>()
            .HasKey(te => te.Codigo);

        base.OnModelCreating(modelBuilder);
    }
}
