using System.Collections.Generic;
using Venda.Crosscutting.Models;

namespace Venda.Crosscutting.Interfaces
{
    public interface IValidavel
    {
        bool EhValido();
        IEnumerable<MensagemErro> PegarMensagensErro();
    }
}