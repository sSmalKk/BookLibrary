USE LivrariaDB;
GO

-- Criar a stored procedure para listar livros com filtros
CREATE PROCEDURE spListarLivrosComFiltro
    @Ano INT = NULL,
    @Mes INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        l.Codigo,
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
