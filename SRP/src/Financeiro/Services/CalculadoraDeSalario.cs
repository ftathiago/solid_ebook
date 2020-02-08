using Cadastro.Models;
namespace Financeiro.Services
{
    public class CalculadoraDeSalario
    {
        public decimal CalcularSalarioPorHora(Colaborador colaborador, decimal valorHora)
        {
            var horasTrabalhadas = colaborador.ConsultarHorasTrabalhadas();
            return horasTrabalhadas * valorHora;
        }
    }
}