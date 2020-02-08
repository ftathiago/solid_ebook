using Venda.Application.App;
using Venda.Application.App.Impl;
using Venda.Application.Factories.Impl;
using Venda.Application.Models;
using Venda.Application.Modules;
using Venda.Dominio.Services;
using Venda.Crosscutting.Models;
using System.Collections.Generic;
using Xunit;
using Moq;
using Venda.Dominio.Entities;
using AutoMapper;

namespace Venda.Application.Test.App
{
    public class VendaApplicationTest
    {

        private const int DINHEIRO = 1;
        [Fact]
        public void EhPossivelCriarVendaApplication()
        {
            var salvarVendaService = new Mock<ISalvarVendaService>();
            IMapper mapper = PegarMapper();
            IVendaApplication vendaApplication = new VendaApplication(new VendaEntityFactory(mapper), salvarVendaService.Object);

            Assert.NotNull(vendaApplication);
        }

        [Fact]
        public void DeveProcessarAVenda()
        {
            var salvarVendaService = new Mock<ISalvarVendaService>();
            salvarVendaService
                .Setup(s => s.Executar(It.IsAny<VendaEntity>()))
                .Returns(true);
            IMapper mapper = PegarMapper();
            IVendaApplication vendaApplication = new VendaApplication(new VendaEntityFactory(mapper), salvarVendaService.Object);
            var vendaDTO = PegarVendaDTO();

            bool vendaEfetuadaComSucesso = vendaApplication.ProcessarVenda(vendaDTO);

            Assert.True(vendaEfetuadaComSucesso);
        }

        [Fact]
        public void NaoProcessaQuandoServiceRetornaFalso()
        {
            var mensagemDeErro = new List<MensagemErro> {
                new MensagemErro("Mensagem de erro")
            };
            var salvarVendaService = new Mock<ISalvarVendaService>(MockBehavior.Strict);
            salvarVendaService
                .Setup(s => s.Executar(It.IsAny<VendaEntity>()))
                .Returns(false);
            salvarVendaService
                .Setup(s => s.PegarMensagensErro())
                .Returns(mensagemDeErro);
            var vendaDTO = PegarVendaDTO();
            IMapper mapper = PegarMapper();
            IVendaApplication vendaApplication = new VendaApplication(new VendaEntityFactory(mapper), salvarVendaService.Object);

            bool vendaEfetuadaComSucesso = vendaApplication.ProcessarVenda(vendaDTO);

            Assert.False(vendaEfetuadaComSucesso);
        }

        [Fact]
        public void DeveRetornarTodasAsMensagensDeErroDoService()
        {
            IEnumerable<MensagemErro> listaDeErros = new List<MensagemErro> {
                new MensagemErro("Mensagem de erro 1"),
                new MensagemErro("Mensagem de erro 2")
            };
            var salvarVendaService = new Mock<ISalvarVendaService>(MockBehavior.Strict);
            salvarVendaService
                .Setup(s => s.Executar(It.IsAny<VendaEntity>()))
                .Returns(false);
            salvarVendaService
                .Setup(s => s.PegarMensagensErro())
                .Returns(listaDeErros);
            var vendaDTO = PegarVendaDTO();
            IMapper mapper = PegarMapper();
            IVendaApplication vendaApplication = new VendaApplication(new VendaEntityFactory(mapper), salvarVendaService.Object);

            bool vendaEfetuadaComSucesso = vendaApplication.ProcessarVenda(vendaDTO);
            var listaDeErrosEncontrados = vendaApplication.PegarMensagensErro();

            Assert.False(vendaEfetuadaComSucesso);
            Assert.Equal(listaDeErros, listaDeErrosEncontrados);
        }

        private VendaModel PegarVendaDTO()
        {
            return new VendaModel()
            {
                Cliente = new ClienteModel("Cliente"),
                FormaDePagamento = DINHEIRO,
                Itens = new List<VendaItemModel>{
                    new VendaItemModel{
                        Descricao = "Produto",
                        QuantidadeComprada = 10,
                        QuantidadePromocional = 20,
                        ValorUnitario = 10,
                        ValorUnitarioPromocional = 5
                    }
                }
            };
        }

        private IMapper PegarMapper()
        {
            return AutoMapperConfig.PegarMapper();
        }
    }
}
