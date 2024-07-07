SET IDENTITY_INSERT Carrinhos ON;

IF NOT EXISTS (SELECT * FROM Carrinhos WHERE Id = 1)
BEGIN
    INSERT INTO Carrinhos (Id, TotalItens, ValorTotal)
    VALUES (1, 0, 0);
END

SET IDENTITY_INSERT Carrinhos OFF;