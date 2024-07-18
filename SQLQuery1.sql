-- Create the Livro table
CREATE TABLE Livro (
    Codigo INT PRIMARY KEY,
    Titulo VARCHAR(255),
    Autor VARCHAR(255),
    Lancamento DATE
);

-- Create the LivroDigital table
CREATE TABLE LivroDigital (
    Codigo INT PRIMARY KEY REFERENCES Livro(Codigo),
    Formato VARCHAR(50)
);

-- Create the LivroImpresso table
CREATE TABLE LivroImpresso (
    Codigo INT PRIMARY KEY REFERENCES Livro(Codigo),
    Peso DECIMAL(5,2)
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
