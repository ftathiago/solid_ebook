using Cadastro.Models;
namespace DepartamentoPessoal.Services
{
    public class MedidorBemEstar
    {
        public const int MAXIMO_HORAS_IDEAL = 160;
        public bool EhCargaIdealDeTrabalho(Colaborador colaborador)
        {
            return colaborador.ConsultarHorasTrabalhadas() <= MAXIMO_HORAS_IDEAL;
        }

    }
}