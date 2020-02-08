using Venda.Dominio.Entities;
using Venda.Application.Models;

namespace Venda.Application.Factories
{
    public interface IVendaEntityFactory
    {
        VendaEntity Criar(VendaModel vendaDTO);
    }
}