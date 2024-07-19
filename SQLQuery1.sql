CREATE TABLE Livro (
    Id INT PRIMARY KEY IDENTITY,
    Titulo NVARCHAR(255) NOT NULL,
    Autor NVARCHAR(255) NOT NULL,
    Lancamento DATE NOT NULL,
    Tipo NVARCHAR(50) NOT NULL CHECK (Tipo IN ('Digital', 'Impresso'))
);

CREATE TABLE LivroDigital (
    Id INT PRIMARY KEY,
    Formato NVARCHAR(50),
    FOREIGN KEY (Id) REFERENCES Livro(Id)
);

CREATE TABLE LivroImpresso (
    Id INT PRIMARY KEY,
    NumeroPaginas INT,
    Encadernacao NVARCHAR(50),
    FOREIGN KEY (Id) REFERENCES Livro(Id)
);


-- Create the TipoEncadernacao table
CREATE TABLE TipoEncadernacao (
    Codigo INT PRIMARY KEY,
    Nome VARCHAR(50),
    Descricao VARCHAR(255),
    Formato VARCHAR(50)
);

-- Create the Tag table
CREATE TABLE Tag (
    Codigo INT PRIMARY KEY,
    Descricao VARCHAR(255)
);

-- Create the Possui table (relation between Livro and Tag)
CREATE TABLE Livro_Tag_Possui (
    Livro_Codigo INT REFERENCES Livro(Codigo),
    Tag_Codigo INT REFERENCES Tag(Codigo),
    PRIMARY KEY (Livro_Codigo, Tag_Codigo)
);

-- Create the Possui table (relation between LivroImpresso and TipoEncadernacao)
CREATE TABLE LivroImpresso_TipoEncadernacao_Possui (
    LivroImpresso_Codigo INT REFERENCES LivroImpresso(Codigo),
    TipoEncadernacao_Codigo INT REFERENCES TipoEncadernacao(Codigo),
    PRIMARY KEY (LivroImpresso_Codigo, TipoEncadernacao_Codigo)
);
