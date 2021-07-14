using Facturas.Entidades.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facturas.Servicios.Negocio
{
    public class FacturaNegocio
    {
        #region Validaciones
        private bool ValidarObjeto(Factura objFactura)
        {
            bool Continuar = true;
            try
            {
                if (objFactura != null)
                {
                    if (objFactura.Cliente == null)
                    {
                        throw new Exception("No se recibió la información del cliente para generar la factura.");
                    }
                   else if (objFactura.Productos == null || !objFactura.Productos.Any())
                    {
                        throw new Exception("No se recibió la información de los productos para generar la factura.");
                    }
                    else if (objFactura.idtipoPago <= 0)
                    {
                        throw new Exception("El tipo de pago no es válido.");
                    }
                    else if (objFactura.IVA <= 0)
                    {
                        throw new Exception("El valor para el IVA  no es válido.");
                    }
                    else if (objFactura.subTotal < 0)
                    {
                        throw new Exception("El valor para el subtotal  no es válido.");
                    }
                    else if (objFactura.Total < 0)
                    {
                        throw new Exception("El valor total  no es válido.");
                    }
                    else if (objFactura.totalDescuento < 0)
                    {
                        throw new Exception("El valor total  de descuento no es válido.");
                    }
                    else if (objFactura.totalImpuesto < 0)
                    {
                        throw new Exception("El valor total  del impuesto no es válido.");
                    }
                    else if (objFactura.descuento < 0)
                    {
                        throw new Exception("El valor del descuento  no es válido.");
                    }
                }
                else
                {
                    throw new Exception("No se recibió la información de la factura.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Continuar;
        }
        #endregion
        #region ParametrosBaseDatos
        public Dictionary<string, object> CargarParametros(Cliente objCliente)
        {
            Dictionary<string, object> lstParametros = new Dictionary<string, object>();
            try
            {
                if (objCliente.idCliente > 0)
                {
                    lstParametros.Add("@idCliente", objCliente.idCliente);
                }
                if (string.IsNullOrEmpty(objCliente.PrimerApellido))
                {
                    lstParametros.Add("@PrimerApellido", DBNull.Value);
                }
                else
                {
                    lstParametros.Add("@PrimerApellido", objCliente.PrimerApellido);
                }
                if (string.IsNullOrEmpty(objCliente.SegundoNombre))
                {
                    lstParametros.Add("@SegundoNombre", DBNull.Value);
                }
                else
                {
                    lstParametros.Add("@SegundoNombre", objCliente.SegundoNombre);
                }
                if (string.IsNullOrEmpty(objCliente.SegundoApellido))
                {
                    lstParametros.Add("@SegundoApellido", DBNull.Value);
                }
                else
                {
                    lstParametros.Add("@SegundoApellido", objCliente.SegundoApellido);
                }
                lstParametros.Add("@idTipoIdentificacion", objCliente.idTipoIdentificacion);
                lstParametros.Add("@numeroIdentificacion", objCliente.numeroIdentificacion);
                lstParametros.Add("@PrimerNombre", objCliente.PrimerNombre);
            }
            catch (Exception)
            {
                throw;
            }
            return lstParametros;
        }
        #endregion
    }
}
