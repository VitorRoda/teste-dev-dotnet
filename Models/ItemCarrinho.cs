using System;

namespace CarrinhoProjeto.Models
{
    public class ItemCarrinho
    {
        public int Id { get; set; }
        public int CarrinhoId { get; set; }
        public string? Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal => Quantidade * PrecoUnitario;
    }
}
