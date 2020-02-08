using System;

namespace Venda.Dominio.ValueObjects
{
    public class ValorUnitario
    {
        public decimal Value => _valor;
        private readonly decimal _valor;
        public ValorUnitario(decimal valor)
        {
            _valor = valor;
        }

        public bool Validar()
        {
            return _valor > 0;
        }

        public static implicit operator ValorUnitario(decimal valor)
        {
            return new ValorUnitario(valor);
        }

        public static implicit operator decimal(ValorUnitario valorUnitario)
        {
            return valorUnitario.Value;
        }
    }
}