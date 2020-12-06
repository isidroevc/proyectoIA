using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.Data
{
    class Sesion
    {
        public string Id { get; set; }
        public string IdGrupo { get; set; }
        public DateTime Fecha { get; set; }
        public string[] Asistencias {get; set;}


    }
}
