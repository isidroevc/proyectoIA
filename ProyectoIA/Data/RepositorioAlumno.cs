using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.Data
{
    class RepositorioAlumno : IRepositorio
    {
        public SQLiteConnection Connection { get; set; }
        public RepositorioAlumno()
        {
            Connection = new SQLiteConnection("Data Source=database.db;Version=3;");
            Connection.Open();
        }

        public Alumno Crear(Alumno alumno)
        {
            Guid guid = Guid.NewGuid();
            alumno.Id = guid.ToString();
            string sql = @"
                INSERT INTO alumnos (id, id_grupo, numero_control, nombre, primer_apellido, segundo_apellido) 
                VALUES (@id, @id_grupo, @numero_control, @nombre, @primer_apellido, @segundo_apellido)
            ";
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", alumno.Id);
            command.Parameters.AddWithValue("@id_grupo", alumno.IdGrupo);
            command.Parameters.AddWithValue("@numero_control", alumno.NumeroControl);
            command.Parameters.AddWithValue("@nombre", alumno.Nombre);
            command.Parameters.AddWithValue("@primer_apellido", alumno.PrimerApellido);
            command.Parameters.AddWithValue("@segundo_apellido", alumno.SegundoApellido);
            command.ExecuteNonQuery();
            return alumno;
        }

        public List<Alumno> ObtenerAlumnosDeUnGrupo(string idGrupo)
        {
            List<Alumno> resultado = new List<Alumno>();
            string sql = @"
                SELECT id, id_grupo, numero_control, nombre, primer_apellido, segundo_apellido FROM alumnos WHERE id_grupo = $idGrupo
            ";
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("$idGrupo", idGrupo);
            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.HasRows && dataReader.Read())
            {
                Alumno nuevoAlumno = new Alumno()
                {
                    Id = dataReader.GetString(0),
                    IdGrupo = dataReader.GetString(1),
                    NumeroControl = dataReader.GetString(2),
                    Nombre = dataReader.GetString(3),
                    PrimerApellido = dataReader.GetString(4),
                    SegundoApellido = dataReader.GetString(5)
                };
                resultado.Add(nuevoAlumno);
            }
            return resultado;
        }
    }
}
