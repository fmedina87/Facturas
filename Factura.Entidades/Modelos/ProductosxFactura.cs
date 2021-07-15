using System;
using System.Collections.Generic;
using System.Text;

namespace Facturas.Entidades.Modelos
{
    public class ProductosxFactura
    {
        public int idProductosxFactura { get; set; }
        public int idProducto { get; set; }
        public int idFactura { get; set; }
        public int cantidadProductos { get; set; }
        public double valorUnitario { get; set; }
        public double valorTotal { get; set; }
        public string Producto { get; set; }
    }
}
