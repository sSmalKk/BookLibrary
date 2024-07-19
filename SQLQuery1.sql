USE LivrariaDb;
GO

-- Verificar e excluir tabelas existentes (se necessário)
IF OBJECT_ID('dbo.LivroImpresso_TipoEncadernacao_Possui', 'U') IS NOT NULL DROP TABLE dbo.LivroImpresso_TipoEncadernacao_Possui;
IF OBJECT_ID('dbo.Livro_Tag_Possui', 'U') IS NOT NULL DROP TABLE dbo.Livro_Tag_Possui;
IF OBJECT_ID('dbo.Tag', 'U') IS NOT NULL DROP TABLE dbo.Tag;
IF OBJECT_ID('dbo.TipoEncadernacao', 'U') IS NOT NULL DROP TABLE dbo.TipoEncadernacao;
IF OBJECT_ID('dbo.LivroImpresso', 'U') IS NOT NULL DROP TABLE dbo.LivroImpresso;
IF OBJECT_ID('dbo.LivroDigital', 'U') IS NOT NULL DROP TABLE dbo.LivroDigital;
IF OBJECT_ID('dbo.Livro', 'U') IS NOT NULL DROP TABLE dbo.Livro;

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
