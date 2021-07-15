using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios.Interfaces.Acciones
{
    public interface ICrear<T, Y> where T : class
    {
        Y crear(T t);
    }
}
