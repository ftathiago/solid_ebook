using System;
using Xunit;
using Financeiro.Services;
using Cadastro.Models;

namespace Financeiro.Test
{
    public class CalculadoraDeSalarioTest
    {
        private const int HORAS_TRABALHADAS_NO_MES = 160;
        private const int VALOR_HORA = 2;

        [Fact]
        public void Should_Create()
        {
            CalculadoraDeSalario calculadoraDeSalario = new CalculadoraDeSalario();

            Assert.NotNull(calculadoraDeSalario);
        }

        [Fact]
        public void Should_CalculateSalaryByHours()
        {
            CalculadoraDeSalario calculadoraDeSalario = new CalculadoraDeSalario();
            Colaborador colaborador = PegarColaborador();
            colaborador.AdicionarHorasTrabalhadas(HORAS_TRABALHADAS_NO_MES);
            decimal salarioEsperado = HORAS_TRABALHADAS_NO_MES * VALOR_HORA;

            decimal salarioCalculado = calculadoraDeSalario.CalcularSalarioPorHora(colaborador, VALOR_HORA);

            Assert.Equal(salarioEsperado, salarioCalculado);
        }

        private Colaborador PegarColaborador()
        {
            return new Colaborador(Guid.NewGuid(), "Teste Nome");
        }
    }
}
