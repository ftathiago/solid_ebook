using System.Collections.Generic;
using Venda.Crosscutting.Models;

namespace Venda.Dominio.DTO
{
    public class VendaDTO
    {
        public VendaDTO()
        {
            Cliente = new ClienteDTO(string.Empty);
            Itens = new List<VendaItemDTO>();
        }
        public ClienteDTO Cliente { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
        public List<VendaItemDTO> Itens { get; set; }
    }
}