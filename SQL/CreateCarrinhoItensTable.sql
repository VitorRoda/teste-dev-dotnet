IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Carrinho_Itens' AND xtype='U')
BEGIN
    CREATE TABLE Carrinho_Itens (
        Id INT PRIMARY KEY IDENTITY,
        CarrinhoId INT FOREIGN KEY REFERENCES Carrinhos(Id),
        Produto NVARCHAR(100) NOT NULL,
        Quantidade INT NOT NULL,
        PrecoUnitario DECIMAL(18, 2) NOT NULL,
        PrecoTotal DECIMAL(18, 2) NOT NULL
    );
END
