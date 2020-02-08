using System;
using Xunit;
using Cadastro.Models;
using Projeto.Services;

namespace Projeto.Test.Services
{
    public class CalculadoraFaturamentoTest
    {
        private const decimal HORAS_TRABALHADAS = 160;
        private const decimal VALOR_HORA = 80;
        [Fact]
        public void Should_Create()
        {
            CalculadoraFaturamento calculadora = new CalculadoraFaturamento();

            Assert.NotNull(calculadora);
        }

        [Fact]
        public void Should_CalculateProjectRevenue()
        {
            CalculadoraFaturamento calculadora = new CalculadoraFaturamento();
            Colaborador colaborador = PegarColaborador();
            colaborador.AdicionarHorasTrabalhadas(HORAS_TRABALHADAS);
            var calculoEsperado = HORAS_TRABALHADAS * VALOR_HORA;

            decimal valorCalculado = calculadora.FaturadoPor(colaborador, VALOR_HORA);

            Assert.Equal(calculoEsperado, valorCalculado);
        }

        private Colaborador PegarColaborador()
        {
            return new Colaborador(Guid.NewGuid(), "Teste Nome");
        }
    }
}