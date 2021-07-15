using System;
using System.Collections.Generic;
using System.Text;

namespace Facturas.Servicios.Interfaces
{
    public interface IRepositorio
    {
        public ICliente Cliente { get; }
     //   public IFactura Factura { get; }
        public IProductosxFactura ProductosxFactura { get; }
    }
}
