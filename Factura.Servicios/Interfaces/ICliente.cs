using Facturas.Entidades.Modelos;
using Facturas.Servicios.Interfaces.Acciones;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturas.Servicios.Interfaces
{
    public interface ICliente: ICrear<Cliente, int>, IConsultarxId<Cliente>
    {
        Task<Cliente> ConsultarxNumeroIdentificacion(string numeroIdentificacion);
    }
}
