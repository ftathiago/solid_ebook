using Venda.Dominio.Entities;

namespace Venda.Dominio.Repository
{
    public interface IVendaRepository
    {
        bool Salvar(VendaEntity venda);
    }
}