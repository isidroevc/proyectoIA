using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.Data
{
    class RepositorioSesiones : IRepositorio
    {
        public SQLiteConnection Connection { get; set; }
        public RepositorioSesiones()
        {
            Connection = new SQLiteConnection("Data Source=database.db;Version=3;");
            Connection.Open();
        }

        public Sesion Crear(Sesion sesion)
        {
            sesion.Id = Guid.NewGuid().ToString();
            string sql = @"
                INSERT INTO sesiones (id, id_grupo, fecha, asistencias) VALUES (@id, @id_grupo, @fecha, @asistencias)
            ";
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", sesion.Id);
            command.Parameters.AddWithValue("@id_grupo", sesion.IdGrupo);
            command.Parameters.AddWithValue("@fecha", sesion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@asistencias", String.Join(",", sesion.Asistencias));
            command.ExecuteNonQuery();
            return sesion;
        }

        public List<Sesion> obtenerSesionesPorGrupo(string idGrupo)
        {
            List<Sesion> resultado = new List<Sesion>();
            string sql = @"
                SELECT id, id_grupo, fecha, asistencias FROM sesiones WHERE id_grupo = $idGrupo
            ";
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("$idGrupo", idGrupo);
            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.HasRows && dataReader.Read())
            {
                Sesion sesion = new Sesion()
                {
                    Id = dataReader.GetString(0),
                    IdGrupo = dataReader.GetString(1),
                    Fecha = DateTime.Parse(dataReader.GetString(2)),
                    Asistencias = dataReader.GetString(3).Split(',')
                };
                resultado.Add(sesion);
            }
            return resultado;
        }
    }
}
