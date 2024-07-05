IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Carrinhos' AND xtype='U')
BEGIN
    CREATE TABLE Carrinhos (
        Id INT PRIMARY KEY IDENTITY,
        TotalItens INT NOT NULL,
        ValorTotal DECIMAL(18, 2) NOT NULL
    );
END