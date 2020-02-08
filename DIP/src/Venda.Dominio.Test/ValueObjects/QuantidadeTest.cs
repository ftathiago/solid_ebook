using Venda.Dominio.ValueObjects;
using Xunit;

namespace Venda.Dominio.Test.ValueObjects
{
    public class QuantidadeTest
    {
        [Fact]
        public void TestCriarQuantidade()
        {
            Quantidade quantidade = new Quantidade(0.001M);

            Assert.NotNull(quantidade);
        }

        [Fact]
        public void TestAtribuicaoDireita()
        {
            const decimal quantidadeEsperada = 10.001M;
            Quantidade quantidade = quantidadeEsperada;

            Assert.Equal(quantidadeEsperada, quantidade.Value);
        }

        [Fact]
        public void TestQuantidadeValida()
        {
            Quantidade quantidade = new Quantidade(0.001M);

            bool estaValido = quantidade.Validar();

            Assert.True(estaValido);
        }

        [Fact]
        public void TestQuantidadeInvalida()
        {
            Quantidade quantidade = new Quantidade(0.000M);

            bool estaValido = quantidade.Validar();

            Assert.False(estaValido);
        }

        [Fact]
        public void TestAtribuicaoEsquerda()
        {
            const decimal quantidadeInicial = 10M;
            Quantidade quantidade = new Quantidade(quantidadeInicial);

            decimal quantidadeCast = quantidade;

            Assert.Equal(quantidadeInicial, quantidadeCast);
        }
    }
}