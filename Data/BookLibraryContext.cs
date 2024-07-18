using Microsoft.EntityFrameworkCore;
using BookLibraryAPI.Model;

namespace BookLibraryAPI.Data
{
    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext(DbContextOptions<BookLibraryContext> options) : base(options) { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TipoEncadernacao> TipoEncadernacoes { get; set; }
        public DbSet<LivroDigital> LivrosDigitais { get; set; }
        public DbSet<LivroImpresso> LivrosImpressos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .HasOne(l => l.LivroDigital)
                .WithOne(ld => ld.Livro)
                .HasForeignKey<LivroDigital>(ld => ld.Codigo);

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.LivroImpresso)
                .WithOne(li => li.Livro)
                .HasForeignKey<LivroImpresso>(li => li.Codigo);

            modelBuilder.Entity<Livro>()
                .HasMany(l => l.LivroTags)
                .WithOne(lt => lt.Livro)
                .HasForeignKey(lt => lt.Livro.Codigo);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Livros)
                .WithOne(lt => lt.Tag)
                .HasForeignKey(lt => lt.Tag.Codigo);

            modelBuilder.Entity<LivroImpresso>()
                .HasMany(li => li.TipoEncadernacoes)
                .WithOne(lei => lei.LivroImpresso)
                .HasForeignKey(lei => lei.LivroImpresso.Codigo);

            modelBuilder.Entity<TipoEncadernacao>()
                .HasMany(te => te.LivroImpressos)
                .WithOne(lei => lei.TipoEncadernacao)
                .HasForeignKey(lei => lei.TipoEncadernacao.Codigo);
        }
    }
}
