using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ProyectoIA.Data
{
    interface IRepositorio
    {
        SQLiteConnection Connection { get; set; }
        
         
    }
}
