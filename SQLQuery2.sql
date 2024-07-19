USE LivrariaDb;
GO

-- Criar a stored procedure para listar livros com filtros
CREATE PROCEDURE spListarLivrosComFiltro
    @Ano INT = NULL,
    @Mes INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        l.Id AS Codigo,
        l.Titulo,
        l.Autor,
        l.Lancamento,
        ISNULL(ld.Formato, 'N/A') AS FormatoDigital,
        ISNULL(li.NumeroPaginas, 0) AS NumeroPaginasImpresso,
        ISNULL(li.Encadernacao, 'N/A') AS EncadernacaoImpresso,
        ISNULL(
            STUFF(
                (SELECT ', ' + t.Descricao
                 FROM dbo.Livro_Tag_Possui lt
                 JOIN dbo.Tag t ON lt.Tag_Codigo = t.Codigo
                 WHERE lt.Livro_Id = l.Id
                 FOR XML PATH('')), 1, 2, ''
            ), 'Nenhuma'
        ) AS Tags
    FROM dbo.Livro l
    LEFT JOIN dbo.LivroDigital ld ON l.Id = ld.Id
    LEFT JOIN dbo.LivroImpresso li ON l.Id = li.Id
    WHERE (@Ano IS NULL OR YEAR(l.Lancamento) = @Ano)
      AND (@Mes IS NULL OR MONTH(l.Lancamento) = @Mes);
END
GO
