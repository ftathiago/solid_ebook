namespace Venda.Application.Models
{
    public class VendaItemModel
    {
        public string Descricao { get; set; }
        public decimal QuantidadeComprada { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal QuantidadePromocional { get; set; }
        public decimal ValorUnitarioPromocional { get; set; }
        public VendaItemModel()
        {
            Descricao = string.Empty;
        }
    }
}