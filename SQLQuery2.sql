CREATE PROCEDURE spListarLivrosComFiltro
    @Ano INT = NULL,
    @Mes INT = NULL
AS
BEGIN
    SELECT 
        l.Codigo,
        l.Titulo,
        l.Autor,
        l.Lancamento,
        ld.Formato AS FormatoDigital,
        li.Peso AS PesoImpresso
    FROM Livro l
    LEFT JOIN LivroDigital ld ON l.Codigo = ld.Codigo
    LEFT JOIN LivroImpresso li ON l.Codigo = li.Codigo
    WHERE (@Ano IS NULL OR YEAR(l.Lancamento) = @Ano)
      AND (@Mes IS NULL OR MONTH(l.Lancamento) = @Mes);
END
