using Facturas.DataBase.Interfaces;
using Facturas.DataBase.Negocio;
using Facturas.Entidades.Modelos;
using Facturas.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios.Negocio
{
    public class ProductosxFacturaNegocio:ComandosDB, IProductosxFactura
    {
        private IAccesoDB _dbAcces { get; }
        public ProductosxFacturaNegocio(IAccesoDB dbAccess)
        {
            _context = dbAccess._context;
            _transaction = dbAccess._transaction;
            _dbAcces = dbAccess;
        }
        #region Validar
        private bool ValidarObjeto(ProductosxFactura objProductosxFactura)
        {
            bool Continuar = true;
            try
            {
                if (objProductosxFactura != null)
                {
                    if (objProductosxFactura.idFactura <= 0)
                    {
                        throw new Exception("No se obtuvo el identificador de la factura.");
                    }
                    else if (objProductosxFactura.idProducto <= 0)
                    {
                        throw new Exception("No se obtuvo el identificador de la factura. Por favor valide e intente nuevamente.");
                    }
                    else if (objProductosxFactura.valorUnitario <= 0)
                    {
                        throw new Exception(string.Format("No se obtuvo el valor unitario del producto {0} . Por favor valide e intente nuevamente.", objProductosxFactura.idProducto));
                    }
                    else if (objProductosxFactura.cantidadProductos <= 0)
                    {
                        throw new Exception(string.Format("La cantidad de productos relacionados no es válida, debe ser mayor de 0. Por favor valide e intente nuevamente.", objProductosxFactura.idProducto));
                    }
                }
                else
                {
                    throw new Exception("No se obtuvo la información para crear la asociación de los productos a la factura.");
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
        public Dictionary<string, object> CargarParametros(ProductosxFactura objProductosxFactura)
        {
            Dictionary<string, object> lstParametros = new Dictionary<string, object>();
            try
            {                
                lstParametros.Add("@idFactura", objProductosxFactura.idFactura);
                lstParametros.Add("@idProducto", objProductosxFactura.idProducto);
                lstParametros.Add("@valorUnitario", objProductosxFactura.valorUnitario);
                lstParametros.Add("@cantidadProductos", objProductosxFactura.cantidadProductos);
            }
            catch (Exception)
            {
                throw;
            }
            return lstParametros;
        }
        #endregion
        #region Crear
        public  int crear(ProductosxFactura objProductosxFactura)
        {
            int idProductosxFactura = 0;            
            try
            {
                if (ValidarObjeto(objProductosxFactura))
                {

                    string _Result = string.Empty;
                    _Result =  commandExecuteDB("PA_PRODUCTOSXFACTURA_INSERTAR", CargarParametros(objProductosxFactura), new SqlParameter() { ParameterName = "@Resultado", Value = _Result });
                    if (Convert.ToInt32(_Result) > 0)
                    {                        
                        idProductosxFactura = Convert.ToInt32(_Result);                        
                    }
                    else
                    {
                        throw new Exception("No se obtuvo un identificador válido para la asociación de producto a factura.");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idProductosxFactura;
        }
        #endregion
        #region Consultar
        public async Task<List<ProductosxFactura>> ConsultarProductosFacturaxIdFactura(int idFactura)
        {
            List<ProductosxFactura> lstProductosxFactura = new List<ProductosxFactura>();
            try
            {
                Dictionary<string, object> lstParametros = new Dictionary<string, object>();
                lstParametros.Add("@idFactura", idFactura);
                lstParametros.Add("@TipoConsulta", 1);
                lstProductosxFactura = Utilidades.MapObjectInstance<ProductosxFactura>(await commandExecuteDBAsync("PA_PRODUCTOSXFACTURA_CONSULTAR", lstParametros));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Se presentó el error {0} al consultar los productos de la factura. ", ex.Message));
            }
            return lstProductosxFactura;
        }
        #endregion
    }
}
