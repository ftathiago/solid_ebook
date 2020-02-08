using System.Collections.Generic;
using Venda.Crosscutting.Interfaces;
using Venda.Crosscutting.Models;

namespace Venda.Dominio.Services.Impl
{
    public class BaseService : IValidavel
    {
        private readonly List<MensagemErro> _mensagensErro;

        public BaseService()
        {
            _mensagensErro = new List<MensagemErro>();
        }
        public bool EhValido()
        {
            return _mensagensErro.Count == 0;
        }

        public IEnumerable<MensagemErro> PegarMensagensErro()
        {
            return _mensagensErro;
        }

        protected void RegistrarErro(MensagemErro mensagemErro)
        {
            _mensagensErro.Add(mensagemErro);
        }
    }
}