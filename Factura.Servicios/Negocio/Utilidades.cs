﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Facturas.Servicios.Negocio
{
   public static class Utilidades
    {
        /// <summary>
        ///metodo para convertir un datatable en el tipo de objeto especificado
        /// </summary>
        /// <typeparam name="T">Tipo de objeto destino</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns></returns>
        public static List<T> MapObjectInstance<T>(DataTable dataTable)
        {
            List<T> functionReturnValue = new List<T>();
            try
            {
                string serializedObject = JsonConvert.SerializeObject(dataTable, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                if (serializedObject != null)
                {
                    functionReturnValue = (List<T>)JsonConvert.DeserializeObject(serializedObject, typeof(List<T>));
                }
            }
            catch (JsonSerializationException)
            {
                try
                {
                    string serializedObject = JsonConvert.SerializeObject(dataTable, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var sb = new StringBuilder(serializedObject);
                    sb[0] = string.Empty.FirstOrDefault();
                    sb[serializedObject.Length - 1] = string.Empty.FirstOrDefault();
                    serializedObject = sb.ToString();
                    functionReturnValue = (List<T>)JsonConvert.DeserializeObject(serializedObject, typeof(List<T>));
                }
                catch (Exception exc)
                { throw exc; }
            }
            catch (Exception ex)
            { throw ex; }
            return functionReturnValue;
        }       
    }
}
