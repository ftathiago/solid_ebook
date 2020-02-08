using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Venda.Crosscutting.Models;
using Venda.Dominio.Entities;
using Venda.Dominio.Modules.Impl;
using Venda.Dominio.DTO;
using Moq;
using Xunit;

namespace Venda.Dominio.Test.Entities
{
    public class VendaTest
    {
        /* 
        Toda venda tem um cliente
        A venda, para ser válida, 
            precisa possuir ao menos um produto
            O valor total precisa ser maior que zero (qtd * vlrUnitario || vlrPromocional)
        As formas de pagamento possíveis são:
            - Dinheiro
            - Vale Alimentação
            - Débito
            - Crédito
            - Cheque
        Cada produto possui uma "Quantidade de promoção" que caso sejam vendidos produtos suficientes, será aplicado o valor promocional
            Cada produto possui uma "Quantidade de promoção" que caso sejam vendidos produtos suficientes, será aplicado o valor promocional
            O valor promocional somente será aplicado para pagamentos:
                - Em dinheiro
                - Cartão de débito
                - Vale Alimentação
            Nos demais casos, será feito o cálculo normal.
        A venda só pode ser concluida após o pagamento ser aprovado
        A venda pode ter vários métodos de pagamento
        */
        [Theory]
        [InlineData(FormaDePagamento.None)]
        [InlineData(FormaDePagamento.Dinheiro)]
        [InlineData(FormaDePagamento.ValeAlimentacao)]
        [InlineData(FormaDePagamento.Debito)]
        [InlineData(FormaDePagamento.Credito)]
        [InlineData(FormaDePagamento.Cheque)]
        public void TestCriarVendaComFormaDePagamento(FormaDePagamento formaDePagamento)
        {
            var cliente = new ClienteDTO("Cliente");
            var vendaDTO = new VendaDTO()
            {
                Cliente = cliente,
                FormaDePagamento = formaDePagamento,
                Itens = new List<VendaItemDTO>()
            };

            VendaEntity venda = new VendaEntity(vendaDTO.Cliente, vendaDTO.FormaDePagamento);
            foreach (var item in vendaDTO.Itens)
            {
                var vendaItem = new VendaItemEntity(item, new CalculadoraPrecoVendaItem());
                venda.AdicionarVendaItem(vendaItem);
            }

            ClienteDTO clienteRetornado = venda.Cliente;

            Assert.Equal(formaDePagamento, venda.FormaDePagamento);
            Assert.Equal(cliente, clienteRetornado);
        }

        [Fact]
        public void TestVendaEhValidaComUmProdutoAoMenos()
        {
            var vendaDTO = new VendaDTO
            {
                Cliente = new ClienteDTO("Cliente"),
                FormaDePagamento = FormaDePagamento.Dinheiro,
                Itens = new List<VendaItemDTO>{
                    VendaItemDTOFactory("Produto1", 1, 1)
                }
            };
            VendaEntity venda = VendaFactory(vendaDTO);

            bool vendaEhValida = venda.Validar();

            Assert.True(vendaEhValida);
        }

        [Fact]
        public void TestVendaNaoEhValidaSemProdutos()
        {
            VendaDTO vendaDTO = new VendaDTO
            {
                Cliente = new ClienteDTO("Cliente"),
                FormaDePagamento = FormaDePagamento.Dinheiro
            };

            VendaEntity venda = VendaFactory(vendaDTO);

            bool vendaEhValida = venda.Validar();

            Assert.False(vendaEhValida);
        }

        [Fact]
        public void TestVendaNaoEhValidaComTotalMenorIgualZero()
        {
            VendaDTO vendaDTO = new VendaDTO
            {
                Cliente = new ClienteDTO("Cliente"),
                FormaDePagamento = FormaDePagamento.Dinheiro,
                Itens = new List<VendaItemDTO>{
                    VendaItemDTOFactory("Produto", 0, 1)
                }
            };
            VendaEntity venda = VendaFactory(vendaDTO);

            bool vendaEhValida = venda.Validar();
            decimal totalVendido = venda.TotalVenda();

            Assert.False(vendaEhValida);
            Assert.Equal(0, totalVendido);
        }

        [Fact]
        public void TestVendaNaoEhValidaComFormaDePagamentoIndefinida()
        {
            VendaDTO vendaDTO = new VendaDTO
            {
                Cliente = new ClienteDTO("Cliente"),
                FormaDePagamento = FormaDePagamento.None,
                Itens = new List<VendaItemDTO>{
                    VendaItemDTOFactory("Descricao", 1, 1)
                }
            };
            VendaEntity venda = VendaFactory(vendaDTO);

            var vendaEhValida = venda.Validar();

            Assert.False(vendaEhValida);
        }

        [Fact]
        public void TestCapturaErrosDeVendaItem()
        {
            var vendaItemErros = new List<ValidationResult> { new ValidationResult("Erro no Item de venda") };
            var vendaItemMock = new Mock<VendaItemEntity>(MockBehavior.Strict);
            vendaItemMock
                .Setup(vi => vi.Validate())
                .Returns(vendaItemErros);
            vendaItemMock
                .Setup(vi => vi.ValorTotal(It.IsAny<FormaDePagamento>()))
                .Returns(10);
            VendaEntity venda = new VendaEntity(new ClienteDTO("Cliente"), FormaDePagamento.Dinheiro);
            venda.AdicionarVendaItem(vendaItemMock.Object);

            var listaErrosEncontrados = venda.Validate();

            Assert.Equal(vendaItemErros, listaErrosEncontrados);
        }

        private VendaEntity VendaFactory(VendaDTO vendaDTO)
        {
            var venda = new VendaEntity(vendaDTO.Cliente, vendaDTO.FormaDePagamento);
            if (vendaDTO.Itens == null)
                return venda;
            foreach (var item in vendaDTO.Itens)
            {
                var vendaItem = new VendaItemEntity(item, new CalculadoraPrecoVendaItem());
                venda.AdicionarVendaItem(vendaItem);
            }
            return venda;
        }

        private VendaItemDTO VendaItemDTOFactory(string Descricao, int QuantidadeComprada, int ValorUnitario,
            decimal quantidadePromocional = -1, decimal valorUnitarioPromocional = -1)
        {
            return new VendaItemDTO
            {
                Descricao = Descricao,
                QuantidadeComprada = QuantidadeComprada,
                ValorUnitario = ValorUnitario,
                QuantidadePromocional = quantidadePromocional,
                ValorUnitarioPromocional = valorUnitarioPromocional
            };
        }
    }
}
