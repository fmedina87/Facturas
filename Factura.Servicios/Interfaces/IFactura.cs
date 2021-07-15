using Facturas.Servicios.Interfaces.Acciones;
using System;
using System.Collections.Generic;
using System.Text;
using Facturas.Entidades.Modelos;
using Facturas.Servicios.Negocio;

namespace Facturas.Servicios.Interfaces
{
    public interface IFactura: ICrear<Factura, int>, IConsultarxId<Factura>
    {
        public IRepositorio _repositorio { get; set; }        

    }
}
