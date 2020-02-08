namespace Venda.Application.Models
{
    public class ClienteModel
    {
        public string Descricao { get; set; }
        public ClienteModel(string descricao)
        {
            this.Descricao = descricao;
        }
    }
}