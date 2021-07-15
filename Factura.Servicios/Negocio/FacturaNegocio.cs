using Facturas.DataBase.Interfaces;
using Facturas.DataBase.Negocio;
using Facturas.Entidades.Modelos;
using Facturas.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios.Negocio
{
    public class FacturaNegocio : ComandosDB, IFactura
    {
        private IAccesoDB _dbAcces { get; }
        public IRepositorio _repositorio { get; set; }
        public FacturaNegocio(IAccesoDB dbAccess)
        {
            try
            {
                _context = dbAccess._context;
                _transaction = dbAccess._transaction;
                _dbAcces = dbAccess;
                _repositorio = new RespositorioNegocio(dbAccess);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
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
                        throw new Exception("No se recibió la información del cliente");
                    }
                    else if (objFactura.Productos == null || !objFactura.Productos.Any())
                    {
                        throw new Exception("No se recibió la información de los productos");
                    }
                    else if (objFactura.idtipoPago <= 0)
                    {
                        throw new Exception("El tipo de pago no es válido");
                    }
                    else if (objFactura.IVA <= 0)
                    {
                        throw new Exception("El valor para el IVA  no es válido");
                    }
                    else if (objFactura.subTotal < 0)
                    {
                        throw new Exception("El valor para el subtotal  no es válido");
                    }
                    else if (objFactura.Total < 0)
                    {
                        throw new Exception("El valor total  no es válido");
                    }
                    else if (objFactura.totalDescuento < 0)
                    {
                        throw new Exception("El valor total  de descuento no es válido");
                    }
                    else if (objFactura.totalImpuesto < 0)
                    {
                        throw new Exception("El valor total  del impuesto no es válido");
                    }
                    else if (objFactura.descuento < 0)
                    {
                        throw new Exception("El valor del descuento  no es válido");
                    }
                    else if (string.IsNullOrEmpty(objFactura.numeroFactura))
                    {
                        throw new Exception("El número de la factura no es válido");
                    }
                }
                else
                {
                    throw new Exception("No se recibió la información de la factura");
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
        public Dictionary<string, object> CargarParametros(Factura objFactura)
        {
            Dictionary<string, object> lstParametros = new Dictionary<string, object>();
            try
            {
                if (objFactura != null)
                {
                    lstParametros.Add("@idCliente", objFactura.idCliente);
                    lstParametros.Add("@idtipoPago", objFactura.idtipoPago);
                    lstParametros.Add("@numeroFactura", objFactura.numeroFactura);
                    lstParametros.Add("@subTotal", objFactura.subTotal);
                    lstParametros.Add("@descuento", objFactura.descuento);
                    lstParametros.Add("@IVA", objFactura.IVA);
                    lstParametros.Add("@totalDescuento", objFactura.totalDescuento);
                    lstParametros.Add("@totalImpuesto ", objFactura.totalImpuesto);
                    lstParametros.Add("@Total", objFactura.Total);
                }
                else
                {
                    throw new Exception("no se obtuvo la información de la factura.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstParametros;
        }
        #endregion
        #region Crear
        public int crear(Factura objFactura)
        {
            int idFactura = 0;
            try
            {
                if (ValidarObjeto(objFactura))
                {

                    string _Result = string.Empty;
                    objFactura.idCliente =  _repositorio.Cliente.crear(objFactura.Cliente);
                    _Result = commandExecuteDB("PA_FACTURA_INSERTAR", CargarParametros(objFactura), new SqlParameter() { ParameterName = "@Resultado", Value = _Result });
                    if (Convert.ToInt32(_Result) > 0)
                    {
                        idFactura = Convert.ToInt32(_Result);
                        foreach (ProductosxFactura objProducto in objFactura.Productos)
                        {
                            objProducto.idFactura = idFactura;
                            objProducto.idProducto =  _repositorio.ProductosxFactura.crear(objProducto);
                        }
                        _dbAcces.SaveChange();
                    }
                    else
                    {
                        throw new Exception("No se obtuvo un identificador válido para la factura.");
                    }
                }
            }
            catch (Exception ex)
                {
                throw new Exception(string.Format("No se pudo generar la factura. Se presentó el siguiente error: {0}. Por favor valide e intente nuevamente. ", ex.Message));
            }
            return idFactura;
        }
        #endregion
        #region Consultas
        public async Task<Factura> ConsultarxId(int idFactura)
        {
            Factura objFactura = new Factura();
            try
            {
                if (idFactura >= 0)
                {
                    Dictionary<string, object> lstParametros = new Dictionary<string, object>();
                    lstParametros.Add("@IdFactura", idFactura);
                    lstParametros.Add("@TipoConsulta", 1);
                    objFactura = Utilidades.MapObjectInstance<Factura>( await commandExecuteDBAsync("PA_FACTURA_CONSULTAR", lstParametros)).FirstOrDefault();
                    if (objFactura != null)
                    {
                        if (objFactura.idCliente > 0)
                        {
                            var Cliente = _repositorio.Cliente.ConsultarxId(objFactura.idCliente);
                            if (Cliente.Result != null)
                            {
                                objFactura.Cliente = Cliente.Result;
                            }
                        }
                        if (objFactura.idFactura > 0)
                        {
                            var Productos = _repositorio.ProductosxFactura.ConsultarProductosFacturaxIdFactura(idFactura);
                            if (Productos.Result != null && Productos.Result.Any())
                            {
                                objFactura.Productos = Productos.Result;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("No se encontró una factura con el identificador solicitado.");
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Se presentó el error {0} al consultar la  factura. Por favor valide e intente nuevamente.", ex.Message));
            }
            return objFactura;
        }
        #endregion
    }
}
