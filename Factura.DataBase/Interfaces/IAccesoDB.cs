using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Facturas.DataBase.Interfaces
{
    public interface IAccesoDB: IDisposable
    {
        public SqlConnection _context { get; set; }
        public SqlTransaction _transaction { get; set; }
        //IServicesRepository _repository { get; set; }
        public void SaveChange();
        public void DiscardChange();
    }
}
