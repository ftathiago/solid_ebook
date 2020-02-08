using AutoMapper;
using Venda.Application.Models;
using Venda.Dominio.DTO;
using System;

namespace Venda.Application.Modules
{
    public static class AutoMapperConfig
    {
        public static IMapper PegarMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VendaModel, VendaDTO>()
                    .ForMember(destination => destination.FormaDePagamento,
                        opt =>
                            opt.MapFrom<FormaDePagamento>((source, destination) =>
                            {
                                var enumValido = Enum.IsDefined(typeof(FormaDePagamento), source.FormaDePagamento);
                                if (!enumValido)
                                    throw new ArgumentOutOfRangeException($"O valor {source.FormaDePagamento} é inválido para forma de pagamento!");
                                string formaDePagamento = FormaDePagamento.GetName(typeof(FormaDePagamento), source.FormaDePagamento);
                                return (FormaDePagamento)Enum.Parse(typeof(FormaDePagamento), formaDePagamento);
                            })
                        );
                cfg.CreateMap<VendaItemModel, VendaItemDTO>();
                cfg.CreateMap<ClienteModel, ClienteDTO>();
            });
            return config.CreateMapper();
        }
    }
}