using Venda.Crosscutting.Models;
using Venda.Dominio.Entities;
using Venda.Dominio.ValueObjects;
using Venda.Dominio.Modules;
using Venda.Dominio.Modules.Impl;
using Venda.Dominio.DTO;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Venda.Dominio.Test.Entities
{
    public class VendaItemTest
    {
        [Fact]
        public void TestCriarVendaItem()
        {
            VendaItemDTO vendaItemDTO = ProdutoVendidoFactory("Produto", 1, 10);

            VendaItemEntity vendaItem = new VendaItemEntity(vendaItemDTO, new CalculadoraPrecoVendaItem());

            Assert.NotNull(vendaItem);
        }

        [Fact]
        public void TestVendaItemExpoeValorUnitarioNormal()
        {
            decimal valorEsperado = 10.5M;
            VendaItemDTO vendaItemDTO = ProdutoVendidoFactory(
                descricao: "Produto",
                quantidadeComprada: 1,
                valorUnitario: valorEsperado);
            VendaItemEntity vendaItem = new VendaItemEntity(vendaItemDTO, new CalculadoraPrecoVendaItem());

            decimal valorExposto = vendaItem.ValorUnitario;

            Assert.Equal(valorEsperado, valorExposto);
        }

        [Fact]
        public void TestVendaItemExpoeValorPromocional()
        {
            decimal valorEsperado = 10.5M;
            VendaItemDTO vendaItemDTO = ProdutoVendidoFactory(
                descricao: "Produto",
                quantidadeComprada: 1,
                valorUnitario: 0,
                valorUnitarioPromocional: valorEsperado);
            VendaItemEntity vendaItem = new VendaItemEntity(vendaItemDTO, new CalculadoraPrecoVendaItem());

            decimal valorEsposto = vendaItem.ValorUnitarioPromocional;

            Assert.Equal(valorEsperado, valorEsposto);
        }

        [Fact]
        public void TestVendaItemCalculaValorTotalUsandoCalculadora()
        {
            decimal valorEsperado = 10.5M;
            var mock = new Mock<ICalculadoraPrecoVendaItem>();
            mock.Setup(library => library.Calcular(It.IsAny<FormaDePagamento>(), It.IsAny<Quantidade>(), It.IsAny<ValorUnitario>(),
                It.IsAny<Quantidade>(), It.IsAny<ValorUnitario>()))
                .Returns(valorEsperado);
            ICalculadoraPrecoVendaItem calculadora = mock.Object;
            VendaItemDTO vendaItemDTO = ProdutoVendidoFactory(
                descricao: "Produto",
                quantidadeComprada: 1,
                valorUnitario: 0,
                valorUnitarioPromocional: valorEsperado);
            VendaItemEntity vendaItem = new VendaItemEntity(vendaItemDTO, calculadora);

            decimal valorEsposto = vendaItem.ValorTotal(FormaDePagamento.Dinheiro);

            Assert.Equal(valorEsperado, valorEsposto);
        }

        [Fact]
        public void TesteVendaItemValidaQuantidade()
        {
            var listaErros = new List<ValidationResult>{
                new ValidationResult("Quantidade do item Produto não pode ser igual ou menor a zero")
            };
            var calculadoraMock = new Mock<ICalculadoraPrecoVendaItem>();
            ICalculadoraPrecoVendaItem calculadora = calculadoraMock.Object;
            VendaItemDTO vendaItemDTO = ProdutoVendidoFactory(
                descricao: "Produto",
                quantidadeComprada: 0,
                valorUnitario: 1,
                valorUnitarioPromocional: 0);
            VendaItemEntity vendaItem = new VendaItemEntity(vendaItemDTO, calculadora);

            var mensagemErro = vendaItem.Validate();

            Assert.Equal(listaErros.First().ErrorMessage, mensagemErro.First().ErrorMessage);
        }

        [Fact]
        public void TesteVendaItemValidaDescricao()
        {
            var listaErros = new List<ValidationResult>{
                new ValidationResult("Item não possui descrição")
            };
            var calculadoraMock = new Mock<ICalculadoraPrecoVendaItem>();
            ICalculadoraPrecoVendaItem calculadora = calculadoraMock.Object;
            VendaItemDTO vendaItemDTO = ProdutoVendidoFactory(
                descricao: string.Empty,
                quantidadeComprada: 1,
                valorUnitario: 0,
                valorUnitarioPromocional: 0);
            VendaItemEntity vendaItem = new VendaItemEntity(vendaItemDTO, calculadora);

            var mensagemErro = vendaItem.Validate();

            Assert.Equal(listaErros.First().ErrorMessage, mensagemErro.First().ErrorMessage);
        }
        [Fact]
        public void TesteVendaItemValidaValorUnitario()
        {
            var listaErros = new List<ValidationResult>{
                new ValidationResult("O valor unitário do item Produto não pode ser menor ou igual a zero")
            };
            var calculadoraMock = new Mock<ICalculadoraPrecoVendaItem>();
            ICalculadoraPrecoVendaItem calculadora = calculadoraMock.Object;
            VendaItemDTO vendaItemDTO = ProdutoVendidoFactory(
                descricao: "Produto",
                quantidadeComprada: 1,
                valorUnitario: 0,
                valorUnitarioPromocional: 0);
            VendaItemEntity vendaItem = new VendaItemEntity(vendaItemDTO, calculadora);

            var mensagemErro = vendaItem.Validate();

            Assert.Equal(listaErros.First().ErrorMessage, mensagemErro.First().ErrorMessage);
        }

        private VendaItemDTO ProdutoVendidoFactory(string descricao, decimal quantidadeComprada, decimal valorUnitario, decimal quantidadePromocional = -1, decimal valorUnitarioPromocional = -1)
        {
            return new VendaItemDTO
            {
                Descricao = descricao,
                QuantidadeComprada = quantidadeComprada,
                ValorUnitario = valorUnitario,
                QuantidadePromocional = quantidadePromocional,
                ValorUnitarioPromocional = valorUnitarioPromocional
            };
        }
    }
}