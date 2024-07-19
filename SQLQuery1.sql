-- Certifique-se de que não há conexões abertas com o banco de dados
USE master;
GO

-- Excluir o banco de dados LivrariaDB se ele existir
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'LivrariaDB')
BEGIN
    DROP DATABASE LivrariaDB;
END
GO
