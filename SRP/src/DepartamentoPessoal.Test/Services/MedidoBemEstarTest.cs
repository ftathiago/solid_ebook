using System;
using Xunit;
using DepartamentoPessoal.Services;
using Cadastro.Models;

namespace DepartamentoPessoal.Test
{
    public class MedidorBemEstarTest
    {
        [Fact]
        public void Should_ConstructInstance()
        {
            MedidorBemEstar calculadoraDeCarga = new MedidorBemEstar();

            Assert.NotNull(calculadoraDeCarga);
        }

        [Fact]
        public void Should_CheckIdealWorkLoad()
        {
            MedidorBemEstar calculadora = new MedidorBemEstar();
            Colaborador colaborador = PegarColaborador();
            colaborador.AdicionarHorasTrabalhadas(120);

            bool cargaIdeal = calculadora.EhCargaIdealDeTrabalho(colaborador);

            Assert.True(cargaIdeal);
        }

        [Fact]
        public void Should_HaveAMaximumWorkLoad()
        {
            MedidorBemEstar calculadora = new MedidorBemEstar();
            Colaborador colaborador = PegarColaborador();
            colaborador.AdicionarHorasTrabalhadas(MedidorBemEstar.MAXIMO_HORAS_IDEAL);

            bool cargaIdeal = calculadora.EhCargaIdealDeTrabalho(colaborador);

            Assert.True(cargaIdeal);
        }

        [Fact]
        public void Should_ReturnFalseWhenWorkMoreThenMaximumWorkLoad()
        {
            MedidorBemEstar calculadora = new MedidorBemEstar();
            Colaborador colaborador = PegarColaborador();
            colaborador.AdicionarHorasTrabalhadas(MedidorBemEstar.MAXIMO_HORAS_IDEAL + 1);

            bool cargaIdeal = calculadora.EhCargaIdealDeTrabalho(colaborador);

            Assert.False(cargaIdeal);
        }

        private Colaborador PegarColaborador()
        {
            return new Colaborador(Guid.NewGuid(), "Nome teste");
        }
    }
}
