using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.DataBase.Negocio
{
    public abstract class ComandosDB
    {
        public SqlConnection _context { get; set; }
        public SqlTransaction _transaction { get; set; }
        /// <summary>
        /// función que retorna el resultado de la actualización o inserción de un registro
        /// </summary>
        /// <param name="spName">NOmbre del procedimiento almacenado</param>
        /// <param name="lstInputParameters">listado de parametros de entrada</param>
        /// <param name="outPutParameter">Valor de retorno</param>
        /// <returns></returns>
        public string commandExecuteDB(string spName, Dictionary<string, object> lstInputParameters, SqlParameter outPutParameter)
        {
            string valorParametroSalida = string.Empty;
            try
            {
                SqlCommand comando = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = spName, Connection = _context, Transaction = _transaction };
                if (lstInputParameters.Count > 0)
                {
                    foreach (var item in lstInputParameters)
                    {
                        comando.Parameters.Add(new SqlParameter() { ParameterName = item.Key, Value = item.Value, IsNullable = true });
                    }
                }
                outPutParameter.Direction = ParameterDirection.Output;
                outPutParameter.IsNullable = true;
                comando.Parameters.Add(outPutParameter);
                try
                {
                    var reader =  comando.ExecuteNonQuery();
                    valorParametroSalida = Convert.ToString(comando.Parameters[outPutParameter.ParameterName].Value);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            { throw ex; }
            return valorParametroSalida;
        }
        /// <summary>
        /// función usada para realizar las consultas a base de datos
        /// </summary>
        /// <param name="spName">Procedimiento almacenado</param>
        /// <param name="lstInputParameters">listado de parametros para el pa</param>
        public async Task<DataTable> commandExecuteDBAsync(string spName, Dictionary<string, object> lstInputParameters)
        {
            var dt = new DataTable();
            try
            {
                SqlCommand comando = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = spName, Connection = _context, Transaction = _transaction };
                if (lstInputParameters.Count > 0)
                {
                    foreach (var item in lstInputParameters)
                    {
                        comando.Parameters.Add(new SqlParameter() { ParameterName = item.Key, Value = item.Value, IsNullable = true });
                    }
                }
                var reader = await comando.ExecuteReaderAsync();
                dt.Load(reader);
                return dt;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
