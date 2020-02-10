using System.Linq;
using System.Collections.Generic;
using System;

namespace Cadastro.Models
{
    public class Colaborador
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public Colaborador(Guid id, string nome)
        {
            _horasDecimal = new List<decimal>();
        }

        public void AdicionarHorasTrabalhadas(decimal qtdHoras)
        {
            _horasDecimal.Add(qtdHoras);
        }
        public decimal ConsultarHorasTrabalhadas()
        {
            return _horasDecimal.Sum(x => x);
        }
        private readonly List<decimal> _horasDecimal;
    }
}