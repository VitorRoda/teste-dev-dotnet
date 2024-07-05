using System.Data;
using Dapper;
using CarrinhoProjeto.Data;
using CarrinhoProjeto.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarrinhoProjeto.Repositories
{
    public class CarrinhoRepository
    {
        private readonly DapperContext _context;

        public CarrinhoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddItemAsync(int carrinhoId, ItemCarrinho item)
        {
            var query = @"INSERT INTO Carrinho_Itens (CarrinhoId, Produto, Quantidade, PrecoUnitario, PrecoTotal)
                          VALUES (@CarrinhoId, @Produto, @Quantidade, @PrecoUnitario, @PrecoTotal);

                          UPDATE Carrinhos
                          SET TotalItens = TotalItens + 1, ValorTotal = ValorTotal + @PrecoTotal
                          WHERE Id = @CarrinhoId;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new
                {
                    CarrinhoId = carrinhoId,
                    item.Produto,
                    item.Quantidade,
                    item.PrecoUnitario,
                    PrecoTotal = item.PrecoUnitario * item.Quantidade
                });
            }
        }

        public async Task<int> RemoveItemAsync(int itemId)
        {
            var itemQuery = "SELECT * FROM Carrinho_Itens WHERE Id = @Id;";
            var deleteQuery = @"DELETE FROM Carrinho_Itens
                                WHERE Id = @Id;

                                UPDATE Carrinhos
                                SET TotalItens = TotalItens - 1, ValorTotal = ValorTotal - @PrecoTotal
                                WHERE Id = @CarrinhoId;";

            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleAsync<ItemCarrinho>(itemQuery, new { Id = itemId });

                return await connection.ExecuteAsync(deleteQuery, new
                {
                    Id = itemId,
                    item.PrecoTotal,
                    item.CarrinhoId
                });
            }
        }

        public async Task<int> UpdateItemAsync(ItemCarrinho item)
        {
            var precoUnitarioQuery = "SELECT PrecoUnitario FROM Carrinho_Itens WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                item.PrecoUnitario = await connection.QuerySingleAsync<decimal>(precoUnitarioQuery, new { Id = item.Id });
            }

            var query = @"UPDATE Carrinho_Itens
                          SET Quantidade = @Quantidade, PrecoTotal = @PrecoTotal
                          WHERE Id = @Id;

                          UPDATE Carrinhos
                          SET ValorTotal = (SELECT SUM(PrecoTotal) FROM Carrinho_Itens WHERE CarrinhoId = @CarrinhoId)
                          WHERE Id = @CarrinhoId;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new
                {
                    item.Quantidade,
                    item.PrecoUnitario,
                    PrecoTotal = item.PrecoUnitario * item.Quantidade,
                    item.Id,
                    item.CarrinhoId
                });
            }
        }

        public async Task<Carrinho> GetCarrinhoAsync(int carrinhoId)
        {
            var query = @"
        SELECT * 
        FROM Carrinhos 
        WHERE Id = @Id;

        SELECT * 
        FROM Carrinho_Itens 
        WHERE CarrinhoId = @Id;";

            using (var connection = _context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { Id = carrinhoId }))
            {
                var carrinho = await multi.ReadSingleOrDefaultAsync<Carrinho>();

                if (carrinho != null)
                {
                    carrinho.Itens = (await multi.ReadAsync<ItemCarrinho>()).AsList() ?? new List<ItemCarrinho>();
                }
                else
                {
                    carrinho = new Carrinho { Id = carrinhoId, Itens = new List<ItemCarrinho>() };
                }
                return carrinho;
            }
        }

    }
}
