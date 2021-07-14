using Facturas.Servicios.Interfaces.Acciones;
using System;
using System.Collections.Generic;
using System.Text;
using Facturas.Entidades.Modelos;

namespace Facturas.Servicios.Interfaces
{
    public interface IFactura: ICrear<Factura, int>, IConsultarxId<Factura>
    {        
    }
}
