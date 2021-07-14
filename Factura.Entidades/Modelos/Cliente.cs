using System;
using System.Collections.Generic;
using System.Text;

namespace Facturas.Entidades.Modelos
{
    public class Cliente
    {        
        public int idCliente { get; set; }
        public int idTipoIdentificacion { get; set; }
        public string numeroIdentificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreCompleto { get; set; }
        public string abreviacionTI { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
