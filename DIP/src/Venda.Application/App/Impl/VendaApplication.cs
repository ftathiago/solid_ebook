using Venda.Dominio.Entities;
using Venda.Dominio.Services;
using Venda.Application.Factories;
using Venda.Application.Models;

namespace Venda.Application.App.Impl
{
    public class VendaApplication : BaseApplication, IVendaApplication
    {
        private readonly IVendaEntityFactory _vendaFactory;
        private readonly ISalvarVendaService _salvarVendaService;

        public VendaApplication(IVendaEntityFactory vendaFactory, ISalvarVendaService salvarVendaService) : base()
        {
            _vendaFactory = vendaFactory;
            _salvarVendaService = salvarVendaService;
        }

        public bool ProcessarVenda(VendaModel vendaModel)
        {
            VendaEntity venda = _vendaFactory.Criar(vendaModel);

            var executouComSucesso = _salvarVendaService.Executar(venda);

            if (!executouComSucesso)
                CarregarErrosDe(_salvarVendaService);

            return executouComSucesso;
        }
    }
}