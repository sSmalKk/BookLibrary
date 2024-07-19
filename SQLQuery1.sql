USE LivrariaDB;
GO

-- Excluir a stored procedure existente, se ela existir
IF OBJECT_ID('dbo.spListarLivrosComFiltro', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.spListarLivrosComFiltro;
END
GO

-- Criar a tabela Livro
CREATE TABLE dbo.Livro (
    Id INT PRIMARY KEY IDENTITY,
    Titulo NVARCHAR(255) NOT NULL,
    Autor NVARCHAR(255) NOT NULL,
    Lancamento DATE NOT NULL
);

-- Criar a tabela LivroDigital
CREATE TABLE dbo.LivroDigital (
    Id INT PRIMARY KEY,  -- Adicionando chave primária
    Formato NVARCHAR(50) NOT NULL,
    FOREIGN KEY (Id) REFERENCES dbo.Livro(Id) -- Relacionamento com Livro
);

-- Criar a tabela LivroImpresso
CREATE TABLE dbo.LivroImpresso (
    Id INT PRIMARY KEY,  -- Adicionando chave primária
    Peso INT NOT NULL,
    FOREIGN KEY (Id) REFERENCES dbo.Livro(Id) -- Relacionamento com Livro
);

-- Criar a tabela TipoEncadernacao
CREATE TABLE dbo.TipoEncadernacao (
    Codigo INT PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL,
    Descricao VARCHAR(255) NOT NULL,
    Formato VARCHAR(50) NOT NULL
);

-- Criar a tabela Tag
CREATE TABLE dbo.Tag (
    Codigo INT PRIMARY KEY,
    Descricao VARCHAR(255) NOT NULL
);

-- Criar a tabela Livro_Tag_Possui
CREATE TABLE dbo.Livro_Tag_Possui (
    Livro_Id INT,
    Tag_Codigo INT,
    PRIMARY KEY (Livro_Id, Tag_Codigo),
    FOREIGN KEY (Livro_Id) REFERENCES dbo.Livro(Id),
    FOREIGN KEY (Tag_Codigo) REFERENCES dbo.Tag(Codigo)
);

-- Criar a tabela LivroImpresso_TipoEncadernacao_Possui
CREATE TABLE dbo.LivroImpresso_TipoEncadernacao_Possui (
    LivroImpresso_Id INT,
    TipoEncadernacao_Codigo INT,
    PRIMARY KEY (LivroImpresso_Id, TipoEncadernacao_Codigo),
    FOREIGN KEY (LivroImpresso_Id) REFERENCES dbo.LivroImpresso(Id),
    FOREIGN KEY (TipoEncadernacao_Codigo) REFERENCES dbo.TipoEncadernacao(Codigo)
);
GO

-- Criar a stored procedure para listar livros com filtros
CREATE PROCEDURE dbo.spListarLivrosComFiltro
    @Ano INT = NULL,
    @Mes INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        l.Id,  -- Corrigido para Id
        l.Titulo,
        l.Autor,
        l.Lancamento
    FROM 
        dbo.Livro l
    WHERE
        (@Ano IS NULL OR YEAR(l.Lancamento) = @Ano)
        AND (@Mes IS NULL OR MONTH(l.Lancamento) = @Mes)
END;
GO
