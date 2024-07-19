-- Criar o banco de dados LivrariaDB
CREATE DATABASE LivrariaDB;
GO

-- Usar o banco de dados recém-criado
USE LivrariaDB;
GO

-- Criar a tabela Livro
CREATE TABLE dbo.Livro (
    Codigo INT PRIMARY KEY IDENTITY,
    Titulo NVARCHAR(255) NOT NULL,
    Autor NVARCHAR(255) NOT NULL,
    Lancamento DATE NOT NULL
);

-- Criar a tabela LivroDigital
CREATE TABLE dbo.LivroDigital (
    Codigo INT PRIMARY KEY,  -- Adicionando chave primária
    Formato NVARCHAR(50) NOT NULL,
    FOREIGN KEY (Codigo) REFERENCES dbo.Livro(Codigo) -- Relacionamento com Livro
);

-- Criar a tabela LivroImpresso
CREATE TABLE dbo.LivroImpresso (
    Codigo INT PRIMARY KEY,  -- Adicionando chave primária
    Peso INT NOT NULL,
    FOREIGN KEY (Codigo) REFERENCES dbo.Livro(Codigo) -- Relacionamento com Livro
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
    Livro_Codigo INT,
    Tag_Codigo INT,
    PRIMARY KEY (Livro_Codigo, Tag_Codigo),
    FOREIGN KEY (Livro_Codigo) REFERENCES dbo.Livro(Codigo),
    FOREIGN KEY (Tag_Codigo) REFERENCES dbo.Tag(Codigo)
);

-- Criar a tabela LivroImpresso_TipoEncadernacao_Possui
CREATE TABLE dbo.LivroImpresso_TipoEncadernacao_Possui (
    LivroImpresso_Codigo INT,
    TipoEncadernacao_Codigo INT,
    PRIMARY KEY (LivroImpresso_Codigo, TipoEncadernacao_Codigo),
    FOREIGN KEY (LivroImpresso_Codigo) REFERENCES dbo.LivroImpresso(Codigo),
    FOREIGN KEY (TipoEncadernacao_Codigo) REFERENCES dbo.TipoEncadernacao(Codigo)
);
GO

-- Criar a stored procedure para listar livros com filtros
CREATE PROCEDURE dbo.spLivros
    @Ano INT = NULL,
    @Mes INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        l.c,  -- Corrigido para Codigo
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
