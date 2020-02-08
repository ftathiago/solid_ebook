using Venda.Dominio.Repository;
using Venda.Dominio.Entities;
using System.Collections.Generic;
using Venda.Crosscutting.Models;

namespace Venda.Dominio.Services.Impl
{
    public class SalvarVendaService : BaseService, ISalvarVendaService
    {

        private readonly IVendaRepository _vendaRepository;

        public SalvarVendaService(IVendaRepository vendaRepository) : base()
        {
            _vendaRepository = vendaRepository;
        }

        public bool Executar(VendaEntity venda)
        {

            if (!venda.Validar())
            {
                RegistrarErro(new MensagemErro("A venda está invalida!"));
                return false;
            }

            bool salvouVenda = _vendaRepository.Salvar(venda);
            if (!salvouVenda)
                RegistrarErro(new MensagemErro("Não foi possível salvar a venda"));

            return salvouVenda;
        }
    }
}