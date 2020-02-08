
using Venda.Application.Models;

namespace Venda.Application.App
{

    public interface IVendaApplication : IBaseApplication
    {
        bool ProcessarVenda(VendaModel vendaModel);
    }
}