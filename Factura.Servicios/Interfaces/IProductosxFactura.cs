using Facturas.Entidades.Modelos;
using Facturas.Servicios.Interfaces.Acciones;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios.Interfaces
{
    public interface IProductosxFactura: ICrear<ProductosxFactura, int>
    {
        Task<List<ProductosxFactura>> ConsultarProductosFacturaxIdFactura(int IdFactura);
    }
}
