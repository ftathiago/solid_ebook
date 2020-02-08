using Venda.Dominio.Entities;
using Venda.Dominio.DTO;
using Venda.Application.Factories;
using Venda.Application.Factories.Impl;
using Venda.Application.Models;
using Venda.Application.Modules;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Xunit;
using System;

namespace Venda.Application.Test
{
    public class VendaEntityFactoryTest
    {
        private const int DINHEIRO = 1;
        [Fact]
        public void TestCriarFabrica()
        {
            IMapper mapper = PegarMapper();
            IVendaEntityFactory vendaEntityFactory = new VendaEntityFactory(mapper);
            var vendaModel = new VendaModel();
            vendaModel.Cliente = new ClienteModel(string.Empty);

            VendaEntity venda = vendaEntityFactory.Criar(vendaModel);

            Assert.NotNull(venda);
            Assert.IsAssignableFrom<VendaEntity>(venda);
        }

        [Fact]
        public void TestConversaoFormaPagamento()
        {
            IVendaEntityFactory vendaEntityFactory = PegarVendaEntityFactory();
            var vendaModel = new VendaModel();
            vendaModel.Cliente = new ClienteModel(string.Empty);
            vendaModel.FormaDePagamento = DINHEIRO;

            VendaEntity venda = vendaEntityFactory.Criar(vendaModel);

            Assert.Equal(FormaDePagamento.Dinheiro, venda.FormaDePagamento);
        }

        [Fact]
        public void TestConversaoFormaPagamentoInvalida()
        {
            IVendaEntityFactory vendaEntityFactory = PegarVendaEntityFactory();
            var vendaModel = new VendaModel();
            vendaModel.Cliente = new ClienteModel(string.Empty);
            vendaModel.FormaDePagamento = 20;

            Assert.Throws<AutoMapperMappingException>(() =>
                vendaEntityFactory.Criar(vendaModel));
        }

        [Fact]
        public void TestCriarVendaComItem()
        {
            var vendaModel = new VendaModel
            {
                Cliente = new ClienteModel("Cliente"),
                FormaDePagamento = DINHEIRO,
                Itens = new List<VendaItemModel>{
                    new VendaItemModel{
                        Descricao = "Produto1",
                        QuantidadeComprada = 10,
                        QuantidadePromocional = 10,
                        ValorUnitario = 10,
                        ValorUnitarioPromocional = 10
                    },
                    new VendaItemModel {
                        Descricao = "Produto2",
                        QuantidadeComprada = 20,
                        QuantidadePromocional = 20,
                        ValorUnitario = 20,
                        ValorUnitarioPromocional = 20
                    }
                }
            };
            IMapper mapper = PegarMapper();
            IVendaEntityFactory vendaEntityFactory = new VendaEntityFactory(mapper);

            VendaEntity venda = vendaEntityFactory.Criar(vendaModel);

            Assert.Equal(2, venda.Itens.Count());
        }

        private IVendaEntityFactory PegarVendaEntityFactory()
        {
            IMapper mapper = PegarMapper();
            return new VendaEntityFactory(mapper);
        }
        private IMapper PegarMapper()
        {
            return AutoMapperConfig.PegarMapper();
        }
    }
}