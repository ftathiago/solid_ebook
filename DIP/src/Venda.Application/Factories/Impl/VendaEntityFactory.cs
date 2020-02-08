using Venda.Application.Modules;
using Venda.Application.Models;
using Venda.Dominio.Entities;
using Venda.Dominio.DTO;
using Venda.Dominio.Modules.Impl;
using AutoMapper;

namespace Venda.Application.Factories.Impl
{
    public class VendaEntityFactory : IVendaEntityFactory
    {
        private readonly IMapper _mapper;
        public VendaEntityFactory(IMapper mapper)
        {
            _mapper = mapper;
        }
        public VendaEntity Criar(VendaModel vendaModel)
        {
            VendaDTO vendaDTO = _mapper.Map<VendaModel, VendaDTO>(vendaModel);
            var venda = CriarVenda(vendaDTO);
            AdicionarItens(venda, vendaDTO);

            return venda;
        }

        private VendaEntity CriarVenda(VendaDTO vendaDTO)
        {
            return new VendaEntity(vendaDTO.Cliente, vendaDTO.FormaDePagamento);
        }

        private void AdicionarItens(VendaEntity venda, VendaDTO vendaDTO)
        {
            if (vendaDTO.Itens == null)
                return;
            foreach (var item in vendaDTO.Itens)
            {
                var vendaItem = new VendaItemEntity(item, new CalculadoraPrecoVendaItem());
                venda.AdicionarVendaItem(vendaItem);
            }
        }
    }
}