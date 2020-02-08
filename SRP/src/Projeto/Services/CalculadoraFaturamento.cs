using System;
using Cadastro.Models;

namespace Projeto.Services
{
    public class CalculadoraFaturamento
    {
        public decimal FaturadoPor(Colaborador colaborador, decimal valorHora)
        {
            var horasTrabalhadas = colaborador.ConsultarHorasTrabalhadas();
            return horasTrabalhadas * valorHora;
        }
    }
}
