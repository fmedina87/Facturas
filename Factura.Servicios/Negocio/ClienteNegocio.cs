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
    public class ClienteNegocio : ComandosDB, ICliente
    {
        private IAccesoDB _dbAcces { get; }
        public ClienteNegocio(IAccesoDB dbAccess)
        {
            _context = dbAccess._context;
            _transaction = dbAccess._transaction;
            _dbAcces = dbAccess;
        }
        #region Validaciones
        private bool validarExisteCliente(Cliente objCliente)
        {
            bool Existe = false;
            try
            {
                Task<Cliente> objClienteAnterior = ConsultarxId(objCliente.idCliente);
                if(objClienteAnterior!=null && objClienteAnterior.Result != null)
                {
                    if((objCliente.numeroIdentificacion== objClienteAnterior.Result.numeroIdentificacion)&&(objCliente.idTipoIdentificacion == objClienteAnterior.Result.idTipoIdentificacion))
                    {
                        Existe = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Existe;
        }
        private bool ValidarObjeto(Cliente objCliente)
        {
            bool Continuar = true;
            try
            {
                if (objCliente != null && objCliente.idCliente >= 0)
                {
                    if (string.IsNullOrEmpty(objCliente.PrimerNombre))
                    {
                        throw new Exception("El primer nombre es obligatorio. Por favor valide e intente nuevamente");
                    }
                    else if (objCliente.idTipoIdentificacion <= 0)
                    {
                        throw new Exception("El tipo de identificación no es válido. Por favor valide e intente nuevamente");
                    }
                    else if (string.IsNullOrEmpty(objCliente.numeroIdentificacion))
                    {
                        throw new Exception("El número de identificación es obligatorio. Por favor valide e intente nuevamente");
                    }
                }
                else
                {
                    throw new Exception("No se recibió la información para crear el cliente. POr favor valide e intente nuevamente");
                }
            }
            catch (Exception)
            {
                throw;
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
        #region Crear
        public async Task<int> crear(Cliente objCliente)
        {
            int idCliente = 0;     
            try
            {                
                if (ValidarObjeto(objCliente))
                {
                    if (validarExisteCliente(objCliente))
                    {
                        var Actualizado = Actualizar(objCliente);
                        idCliente = objCliente.idCliente;
                    }
                    else
                    {
                        string _Result = string.Empty;
                        _Result = await commandExecuteDBAsync("PA_CLIENTE_INSERTAR", CargarParametros(objCliente), new SqlParameter() { ParameterName = "@Resultado", Value = _Result });
                        if (Convert.ToInt32(_Result) > 0)
                        {
                            idCliente = Convert.ToInt32(_Result);
                        }
                        else
                        {
                            throw new Exception("No se obtuvo un identificador válido para el cliente.");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idCliente;
        }
        #endregion
        #region Actualizar
        private async Task<int> Actualizar(Cliente objCliente)
        {
            int idCliente = 0;
            try
            {
                if (ValidarObjeto(objCliente))
                {
                    string _Result = string.Empty;
                    _Result = await commandExecuteDBAsync("PA_CLIENTE_ACTUALIZAR", CargarParametros(objCliente), new SqlParameter() { ParameterName = "@Resultado", Value = _Result });
                    if (Convert.ToInt32(_Result) > 0)
                    {
                        idCliente = Convert.ToInt32(_Result);
                    }
                    else
                    {
                        throw new Exception("No se obtuvo un identificador válido para el cliente.");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return idCliente;
        }
        #endregion
        #region Consultas
        public async Task<Cliente> ConsultarxId(int idCliente)
        {
            Cliente objCliente = new Cliente();
            try
            {
                Dictionary<string, object> lstParametros = new Dictionary<string, object>();
                lstParametros.Add("@idCliente", idCliente);
                lstParametros.Add("@TipoConsulta", 2);
                objCliente = Utilidades.MapObjectInstance<Cliente>(await commandExecuteDBAsync("PA_CLIENTE_CONSULTAR", lstParametros)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Se presentó el error {0} al consultar el cliente. ", ex.Message));
            }
            return objCliente;
        }
        public async Task<Cliente> ConsultarxNumeroIdentificacion(string numeroIdentificacion)
        {
            Cliente objCliente = new Cliente();
            try
            {
                Dictionary<string, object> lstParametros = new Dictionary<string, object>();
                lstParametros.Add("@numeroIdentificacion", numeroIdentificacion);
                lstParametros.Add("@TipoConsulta", 1);
                objCliente = Utilidades.MapObjectInstance<Cliente>(await commandExecuteDBAsync("PA_CLIENTE_CONSULTAR", lstParametros)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Se presentó el error {0} al consultar el cliente. ", ex.Message));
            }
            return objCliente;
        }
        #endregion
    }
}
