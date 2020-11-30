using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.Data
{
    class RepositorioGrupo : IRepositorio
    {
        public SQLiteConnection Connection { get; set; }
        public RepositorioGrupo()
        {
            Connection = new SQLiteConnection("Data Source=database.db;Version=3;");
            Connection.Open();
        }

        public Grupo Crear(Grupo grupo)
        {
            Guid guid = Guid.NewGuid();
            grupo.Id = guid.ToString();
            string sql = @"
                INSERT INTO grupos (id, numero, nombre) VALUES (@id, @numero, @nombre)
            ";
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", grupo.Id);
            command.Parameters.AddWithValue("@numero", grupo.Numero);
            command.Parameters.AddWithValue("@nombre", grupo.Nombre);

            command.ExecuteNonQuery();
            return grupo;
        }

        public List<Grupo> ObtenerLista()
        {
            List<Grupo> resultado = new List<Grupo>();
            string sql = @"
                SELECT * FROM grupos
            ";
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = sql;
            SQLiteDataReader dataReader = command.ExecuteReader();
            while(dataReader.HasRows && dataReader.Read())
            {
                Grupo nuevoGrupo = new Grupo()
                {
                    Id = dataReader.GetString(0),
                    Numero = dataReader.GetString(1),
                    Nombre = dataReader.GetString(2),
                };
                resultado.Add(nuevoGrupo);
            }
            return resultado;
        }
    }
}
