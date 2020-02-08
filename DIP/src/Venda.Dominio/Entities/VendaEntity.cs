using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Venda.Dominio.DTO;

namespace Venda.Dominio.Entities
{
    public class VendaEntity : BaseEntity
    {
        public ClienteDTO Cliente { get; private set; }
        public FormaDePagamento FormaDePagamento { get; private set; }
        public IEnumerable<VendaItemEntity> Itens { get => _itensLista; }
        private readonly IList<VendaItemEntity> _itensLista;
        protected VendaEntity()
        {
            _itensLista = new List<VendaItemEntity>();
            Cliente = new ClienteDTO(string.Empty);
            this.FormaDePagamento = FormaDePagamento.None;
        }
        public VendaEntity(ClienteDTO? cliente, FormaDePagamento formaDePagamento)
        {
            if (cliente == null)
                throw new ArgumentException("O cliente para esta venda não foi informado!");
            Cliente = cliente;
            _itensLista = new List<VendaItemEntity>();
            FormaDePagamento = formaDePagamento;
        }

        public void AdicionarVendaItem(VendaItemEntity vendaItem)
        {
            _itensLista.Add(vendaItem);
        }

        public decimal TotalVenda()
        {
            var totalVenda = Itens.Sum(p => p.ValorTotal(FormaDePagamento));
            return totalVenda;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Validate();
        }

        public override IEnumerable<ValidationResult> Validate()
        {
            var listaErros = new List<ValidationResult>();

            if (FormaDePagamento == FormaDePagamento.None)
                listaErros.Add(new ValidationResult("Forma de pagamento não informada"));
            if (Itens.Count() == 0)
                listaErros.Add(new ValidationResult("A venda não contém itens"));
            if (TotalVenda() <= 0)
                listaErros.Add(new ValidationResult("O valor total da venda é igual ou inferior a zero"));
            foreach (var vendaItem in _itensLista)
            {
                var mensagensErroItem = vendaItem.Validate();
                if (mensagensErroItem.Count() == 0)
                    continue;
                listaErros.AddRange(mensagensErroItem);
            }

            return listaErros;
        }
    }
}