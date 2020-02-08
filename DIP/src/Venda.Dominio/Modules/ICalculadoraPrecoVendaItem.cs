using Venda.Dominio.ValueObjects;
using Venda.Dominio.DTO;
using Venda.Crosscutting.Models;


namespace Venda.Dominio.Modules
{
    public interface ICalculadoraPrecoVendaItem
    {
        ValorUnitario Calcular(FormaDePagamento formaDePagamento, Quantidade quantidade, ValorUnitario valorUnitario,
            Quantidade? quantidadePromocional = null, ValorUnitario? valorPromocional = null);
    }
}