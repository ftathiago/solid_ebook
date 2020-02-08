using System;
using Venda.Dominio.Repository;
using Venda.Dominio.Entities;

namespace Venda.Infra.Repository
{
    public class VendaRepository : IVendaRepository
    {
        public bool Salvar(VendaEntity venda)
        {
            return true;
        }
    }
}
