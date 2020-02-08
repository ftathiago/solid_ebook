using System;
using Venda.Dominio.ValueObjects;
using Venda.Dominio.DTO;
using Venda.Crosscutting.Models;

namespace Venda.Dominio.Modules.Impl
{
    public class CalculadoraPrecoVendaItem : ICalculadoraPrecoVendaItem
    {
        public CalculadoraPrecoVendaItem() { }

        public ValorUnitario Calcular(FormaDePagamento formaDePagamento, Quantidade quantidade, ValorUnitario valorUnitario,
            Quantidade? quantidadePromocional = null, ValorUnitario? valorPromocional = null)
        {
            if (DeveUsarPrecoVendaNormal(formaDePagamento, quantidade, quantidadePromocional))
                return CalculoPrecoNormal(quantidade, valorUnitario);
            return CalcularPrecoPromocional(quantidade, valorPromocional);
        }

        private bool DeveUsarPrecoVendaNormal(FormaDePagamento formaDePagamento,
            Quantidade quantidade, Quantidade? quantidadePromocional)
        {
            if (!FormaDePagamentoEhValida(formaDePagamento))
                throw new ArgumentException("Forma de pagamento inválida!");

            var formaDePagamentoNormal = FormaDePagamentoDefineCalculoNormal(formaDePagamento);
            var vendaNormalPelaQuantidade = QuantidadeDefineCalculoNormal(quantidade, quantidadePromocional);

            return vendaNormalPelaQuantidade || formaDePagamentoNormal;
        }

        private bool FormaDePagamentoEhValida(FormaDePagamento formaDePagamento)
        {
            return formaDePagamento == FormaDePagamento.Dinheiro ||
                formaDePagamento == FormaDePagamento.ValeAlimentacao ||
                formaDePagamento == FormaDePagamento.Debito ||
                formaDePagamento == FormaDePagamento.Credito ||
                formaDePagamento == FormaDePagamento.Cheque;
        }

        private bool FormaDePagamentoDefineCalculoNormal(FormaDePagamento formaDePagamento)
        {
            if (formaDePagamento == FormaDePagamento.Cheque)
                return true;
            if (formaDePagamento == FormaDePagamento.Credito)
                return true;
            return false;
        }

        private bool QuantidadeDefineCalculoNormal(Quantidade quantidade, Quantidade? quantidadePromocional)
        {
            if (quantidadePromocional == null)
                return true;

            if (!quantidadePromocional.Validar())
                return true;

            return (quantidade.Value < quantidadePromocional.Value);
        }

        private ValorUnitario CalculoPrecoNormal(Quantidade quantidade, ValorUnitario valorUnitario)
        {
            return quantidade * valorUnitario;
        }

        private ValorUnitario CalcularPrecoPromocional(Quantidade quantidade, ValorUnitario? valorPromocional)
        {
            if (valorPromocional == null)
                return 0;
            return quantidade * valorPromocional;
        }
    }
}