namespace Venda.Dominio.DTO
{
    public class VendaItemDTO
    {
        public string Descricao { get; set; }
        public decimal QuantidadeComprada { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal QuantidadePromocional { get; set; }
        public decimal ValorUnitarioPromocional { get; set; }
        public VendaItemDTO()
        {
            Descricao = string.Empty;
        }
    }
}