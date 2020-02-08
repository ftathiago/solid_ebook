using System.Collections.Generic;
using Venda.Crosscutting.Models;
using Venda.Crosscutting.Interfaces;

namespace Venda.Application.App.Impl
{
    public abstract class BaseApplication : IBaseApplication
    {
        private readonly List<MensagemErro> _mensagensErro;

        public BaseApplication()
        {
            _mensagensErro = new List<MensagemErro>();
        }

        public IEnumerable<MensagemErro> PegarMensagensErro()
        {
            foreach (var mensagem in _mensagensErro)
            {
                yield return mensagem;
            }
        }

        public bool EhValido()
        {
            return _mensagensErro.Count == 0;
        }

        protected void CarregarErrosDe(IValidavel validavel)
        {
            foreach (var mensagemErro in validavel.PegarMensagensErro())
            {
                _mensagensErro.Add(mensagemErro);
            }
        }
    }
}