using Microsoft.AspNetCore.Mvc;
using CarrinhoProjeto.Models;
using CarrinhoProjeto.Repositories;

// Segui as rotas exigidas na atividade, entretanto acredito que pra melhor entendimento 
// seria ideal chamar as rotas por nomes mais referentes ao que elas fazem exatamente
// como post /api/carrinho/{id}/itens

namespace CarrinhoProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoRepository _repository;

        public CarrinhoController(CarrinhoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarrinho(int id)
        {
            var carrinho = await _repository.GetCarrinhoAsync(id);
            
            var response = new
            {
                itens = carrinho.Itens.Select(i => new 
                {
                    id = i.Id,
                    nomeProduto = i.Produto,
                    quantidade = i.Quantidade,
                    precoUnitario = i.PrecoUnitario,
                    precoTotal = i.PrecoTotal
                }),
                totalItens = carrinho.TotalItens,
                valorTotal = carrinho.ValorTotal
            };

            return Ok(response);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddItem(int id, [FromBody] ItemCarrinho item)
        {
            await _repository.AddItemAsync(id, item);
            
            var response = new 
            {
                mensagem = "Item adicionado com sucesso",
                item = new 
                {
                    id = item.Id,
                    nomeProduto = item.Produto,
                    quantidade = item.Quantidade,
                    precoUnitario = item.PrecoUnitario,
                    precoTotal = item.PrecoTotal
                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            await _repository.RemoveItemAsync(id);
            return Ok(new { mensagem = "Item removido com sucesso" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemCarrinho item)
        {
            item.Id = id;
            await _repository.UpdateItemAsync(item);
            
            var response = new 
            {
                mensagem = "Quantidade atualizada com sucesso",
                item = new 
                {
                    id = item.Id,
                    nomeProduto = item.Produto,
                    quantidade = item.Quantidade,
                    precoUnitario = item.PrecoUnitario,
                    precoTotal = item.PrecoTotal
                }
            };

            return Ok(response);
        }
    }
}
