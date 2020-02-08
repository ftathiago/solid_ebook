using Venda.Crosscutting.Interfaces;
using Venda.Dominio.Entities;

namespace Venda.Dominio.Services
{
    public interface ISalvarVendaService : IValidavel
    {
        bool Executar(VendaEntity venda);
    }
}