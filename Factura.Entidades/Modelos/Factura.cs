using System;
using System.Collections.Generic;
using System.Text;

namespace Facturas.Entidades.Modelos
{
    public class Factura
    {
        public int idFactura { get; set; }
        public int idtipoPago { get; set; }
        public int idCliente { get; set; }
        public string TipoPago { get; set; }
        public string numeroFactura { get; set; }
        public DateTime fecha { get; set; }
        public double subTotal { get; set; }
        public double IVA { get; set; }
        public double totalDescuento { get; set; }
        public double totalImpuesto { get; set; }
        public double Total { get; set; }
        public double descuento { get; set; }
        public Cliente Cliente { get; set; }        
        public List<ProductosxFactura> Productos { get; set; }
    }
}
