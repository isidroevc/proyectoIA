using ProyectoIA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIA
{
    public partial class VistaSesionesGrupo : Form
    {

        private Grupo grupo;
        private RepositorioSesiones repositorioSesiones;
        private RepositorioAlumno repositorioAlumno;
        private List<Alumno> alumnos;
        private List<Sesion> sesiones;
        private List<string> columnas;
        private string[][] data;
        private Dictionary<string, Sesion> sesionesPorFecha;
        public VistaSesionesGrupo(Grupo grupo)
        {
            this.repositorioAlumno = new RepositorioAlumno();
            this.repositorioSesiones = new RepositorioSesiones();
            this.columnas = new List<string>();
            this.sesionesPorFecha = new Dictionary<string, Sesion>();
            this.alumnos = repositorioAlumno.ObtenerAlumnosDeUnGrupo(grupo.Id);
            this.sesiones = repositorioSesiones.obtenerSesionesPorGrupo(grupo.Id);
            this.grupo = grupo;
            InitializeComponent();
            RefescarTabla();
        }

        private void VistaSesionesGrupo_Load(object sender, EventArgs e)
        {
            label1.Text = $"Grupo - {grupo.Numero} - {grupo.Nombre}";
        }

        private void RefescarTabla()
        {
            this.columnas = new List<string>();
            this.columnas.Add("No. Control");
            this.columnas.Add("Nombre del Alumno");
            int columnasBase = columnas.Count;
            this.sesiones.OrderBy(s => s.Fecha).ToList().ForEach(sesion =>
            {
                string fecha = sesion.Fecha.ToString("yyyy-MM-dd HH:mm:ss");
                this.columnas.Add(fecha);
                this.sesionesPorFecha[fecha] = sesion;
            });
            tablaAsistencias.ColumnCount = this.columnas.Count;
            for(int  i = 0; i < this.columnas.Count; i++)
            {
                tablaAsistencias.Columns[i].Name = this.columnas[i];
            }
            data = new string[this.alumnos.Count][];
            this.alumnos = this.alumnos
                    .OrderBy(a => $"{a.PrimerApellido} {a.SegundoApellido} {a.Nombre}")
                    .ToList();
            for (int i = 0, c = this.alumnos.Count; i < c; i++)
            {
                data[i] = new string[this.sesiones.Count + 2];
                data[i][0] = alumnos[i].NumeroControl;
                data[i][1] = $"{alumnos[i].PrimerApellido} {alumnos[i].SegundoApellido} {alumnos[i].Nombre}";

                for(int j = 0, k = sesiones.Count; j < k; j++)
                {
                    bool asistencia = sesiones[j].Asistencias.Contains(alumnos[i].Id);
                    data[i][j + columnasBase] = asistencia ? "Presente" : "Ausente";
                }
            }
            for(int i = 0, c = data.Length; i < c; i++)
            {
                tablaAsistencias.Rows.Add(data[i]);
                for(int j = 0, k = data[i].Length; j < k; j++)
                {
                    if (data[i][j] == "Presente")
                    {
                        tablaAsistencias[j, i].Style.ForeColor = Color.Green;
                    } else if (data[i][j] == "Ausente")
                    {
                        tablaAsistencias[j, i].Style.ForeColor = Color.Red;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[][] csvData = new string[2 + data.Length][];
            csvData[0] = new string[] { $"Grupo - {grupo.Numero} - {grupo.Nombre}" };
            csvData[1] = this.columnas.ToArray();
            for (int i = 0, c = data.Length; i < c; i++) {
                csvData[i + 2] = data[i];
            }
            StringBuilder resultBuilder = new StringBuilder();
            foreach(string[] row in csvData)
            {
                resultBuilder.Append(String.Join(";", row));
                resultBuilder.Append("\n");
            }
            string filename = $"{grupo.Numero}_{grupo.Nombre}.csv";
            File.WriteAllText($"{grupo.Numero}_{grupo.Nombre}.csv", resultBuilder.ToString());
            Process.Start(filename);
        }
    }
}
