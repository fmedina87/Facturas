using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios.Interfaces.Acciones
{
   public interface IConsultarxId<T> where T : class
    {
        Task<T> ConsultarxId(int Id);
    }
}
