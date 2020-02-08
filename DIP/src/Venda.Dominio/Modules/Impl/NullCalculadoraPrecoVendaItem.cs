using Venda.Crosscutting.Models;
using Venda.Dominio.ValueObjects;
using Venda.Dominio.DTO;

namespace Venda.Dominio.Modules.Impl
{
    public class NullCalculadoraPrecoVendaItem : ICalculadoraPrecoVendaItem
    {
        public ValorUnitario Calcular(FormaDePagamento formaDePagamento, Quantidade quantidade, ValorUnitario valorUnitario, Quantidade? quantidadePromocional = null, ValorUnitario? valorPromocional = null)
        {
            return 0;
        }
    }
}