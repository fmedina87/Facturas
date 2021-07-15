using Facturas.DataBase.Interfaces;
using Facturas.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturas.Servicios.Negocio
{
    public class RespositorioNegocio: IRepositorio
    {
        #region Interfaces
        public ICliente Cliente { get; }
        public IProductosxFactura ProductosxFactura { get; }
        #endregion
        #region Implementacion
       public RespositorioNegocio(IAccesoDB AccesoDB)
        {
            Cliente = new ClienteNegocio(AccesoDB);
            ProductosxFactura = new ProductosxFacturaNegocio(AccesoDB);
        }
        #endregion
    }
}
