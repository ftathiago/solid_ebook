using Venda.Dominio.ValueObjects;
using Venda.Crosscutting.Models;
using Venda.Dominio.Modules.Impl;
using Venda.Dominio.DTO;
using Xunit;
using System;

namespace Venda.Dominio.Test
{
    /*
        Para não haver overhead de criação/destruição de objetos, a instância de 
        calculadora deverá ser reaproveitável
    */
    public class CalculadoraPrecoVendaItemTest
    {
        [Fact]
        public void TestCriarCalculadora()
        {
            var calculadoraPrecoVendaItem = new CalculadoraPrecoVendaItem();

            Assert.NotNull(calculadoraPrecoVendaItem);
        }

        [Theory]
        [InlineData(FormaDePagamento.Dinheiro)]
        [InlineData(FormaDePagamento.ValeAlimentacao)]
        [InlineData(FormaDePagamento.Debito)]
        public void TestQuantidadeGaranteCalculoNormal(FormaDePagamento formaDePagamento)
        {
            Quantidade quantidadeVendidaMenorQuePromocional = new Quantidade(5.5M);
            ValorUnitario valorUnitario = new ValorUnitario(10);
            Quantidade quantidadePromocionalMaiorQueComprado = new Quantidade(5.51M);
            ValorUnitario valorPromocional = new ValorUnitario(20M);
            ValorUnitario valorEsperado = new ValorUnitario(55M);
            var calculadoraPrecoVendaItem = new CalculadoraPrecoVendaItem();

            var valorCalculado = calculadoraPrecoVendaItem.Calcular(
                formaDePagamento, quantidadeVendidaMenorQuePromocional, valorUnitario,
                quantidadePromocionalMaiorQueComprado, valorPromocional);

            Assert.Equal(valorEsperado.Value, valorCalculado.Value);
        }

        [Theory]
        [InlineData(FormaDePagamento.Cheque, 55.2)]
        [InlineData(FormaDePagamento.Credito, 55.2)]
        [InlineData(FormaDePagamento.Dinheiro, 110.40)]
        [InlineData(FormaDePagamento.ValeAlimentacao, 110.40)]
        [InlineData(FormaDePagamento.Debito, 110.40)]
        public void TestFormaDePagamentoGaranteCalculoNormal(FormaDePagamento formaDePagamento,
            decimal valorEsperado)
        {
            Quantidade quantidadeVendidaMaiorQuePromocional = 5.52M;
            ValorUnitario valorUnitario = 10;
            Quantidade quantidadePromocionalMenorQueComprado = 5.51M;
            ValorUnitario valorPromocional = 20M;
            var calculadoraPrecoVendaItem = new CalculadoraPrecoVendaItem();

            var valorCalculado = calculadoraPrecoVendaItem.Calcular(
                formaDePagamento, quantidadeVendidaMaiorQuePromocional, valorUnitario,
                quantidadePromocionalMenorQueComprado, valorPromocional);

            Assert.Equal(valorEsperado, valorCalculado.Value);
        }

        [Theory]
        [InlineData(FormaDePagamento.Dinheiro, 55.2)]
        [InlineData(FormaDePagamento.ValeAlimentacao, 55.2)]
        [InlineData(FormaDePagamento.Debito, 55.2)]
        [InlineData(FormaDePagamento.Credito, 110.4)]
        [InlineData(FormaDePagamento.Cheque, 110.4)]
        public void TestCalculaPrecoPromocional(FormaDePagamento formaDePagamento,
            decimal valorEsperado)
        {
            Quantidade quantidadeVendidaMaiorQuePromocional = 5.52M;
            ValorUnitario valorUnitario = 20;
            Quantidade quantidadePromocionalMenorQueComprado = 5.51M;
            ValorUnitario valorPromocional = 10;
            var calculadoraPrecoVendaItem = new CalculadoraPrecoVendaItem();

            var valorCalculado = calculadoraPrecoVendaItem.Calcular(
                formaDePagamento, quantidadeVendidaMaiorQuePromocional, valorUnitario,
                quantidadePromocionalMenorQueComprado, valorPromocional);

            Assert.Equal(valorEsperado, valorCalculado.Value);
        }

        [Fact]
        public void TestQuantidadePromocionalMenorZeroSempreCalculoNormal()
        {
            Quantidade quantidadeVendidaMaiorQuePromocional = 5.52M;
            ValorUnitario valorUnitario = 10.5M;
            Quantidade quantidadePromocionalMenorZero = -0.01M;
            ValorUnitario valorPromocional = -1;
            ValorUnitario valorEsperado = 57.96M;
            var formaDePagamento = FormaDePagamento.Dinheiro;
            var calculadoraPrecoVendaItem = new CalculadoraPrecoVendaItem();

            var valorCalculado = calculadoraPrecoVendaItem.Calcular(
                formaDePagamento, quantidadeVendidaMaiorQuePromocional, valorUnitario,
                quantidadePromocionalMenorZero, valorPromocional);

            Assert.Equal(valorEsperado.Value, valorCalculado.Value);
        }

        [Fact]
        public void TestCalculadoraEmiteExcecaoAoUsarFormaDePagamentoInvalida()
        {
            var calculadoraPrecoVendaItem = new CalculadoraPrecoVendaItem();

            Assert.Throws<ArgumentException>(() => calculadoraPrecoVendaItem.Calcular(
                FormaDePagamento.None, 0, 0, 0, 0));
        }
    }
}