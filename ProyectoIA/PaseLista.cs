using ProyectoIA.Data;
using ProyectoIA.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIA
{
    public partial class PaseLista : Form
    {
        private Grupo grupo;
        private List<Alumno> alumnos;
        private RepositorioAlumno repositorioAlumno;
        private List<Label> tableLabels;
        private List<Button> tableButtons;
        Dictionary<string, Alumno> alumnosAusentes;
        Dictionary<string, Alumno> alumnosPresentes;
        Dictionary<string, Label> labelMap;
        Dictionary<string, string[]> nameTokensMap;
        ITextExtractor textExtractor;
        int labelWidth;
        int labelHeight;
        public PaseLista(Grupo grupo)
        {
            this.grupo = grupo;
            this.textExtractor = new DefaultTextExtractor(1);
            this.repositorioAlumno = new RepositorioAlumno();
            this.tableLabels = new List<Label>();
            this.tableButtons = new List<Button>();
            this.alumnosPresentes = new Dictionary<string, Alumno>();
            this.alumnosAusentes = new Dictionary<string, Alumno>();
            this.labelMap = new Dictionary<string, Label>();
            this.nameTokensMap = new Dictionary<string, string[]>();
            this.labelWidth = 780;
            this.labelHeight = 24;
            InitializeComponent();
            CargarAlumnos();
            PintarAlumnos();
            textExtractor.StartWorking();
        }

        private void PaseLista_Load(object sender, EventArgs e)
        {

        }

        public void CargarAlumnos()
        {
            this.alumnos = repositorioAlumno.ObtenerAlumnosDeUnGrupo(grupo.Id);

            this.alumnos.ForEach(alumno =>
            {
                this.nameTokensMap.Add(alumno.Id, TokenizeName(alumno));
                this.alumnosAusentes.Add(alumno.Id, alumno);
            });
        }

        public void PintarAlumnos()
        {
            tableLabels.ForEach(label => label.Dispose());
            tableLabels.Clear();

            tableButtons.ForEach(button => button.Dispose());
            tableButtons.Clear();

            for (int i = 0, count = this.alumnos.Count; i < count; i++)
            {
                Label labelNombre = new Label();
                labelNombre.Text = $"{alumnos[i].NumeroControl} - {alumnos[i].PrimerApellido} {alumnos[i].SegundoApellido} {alumnos[i].Nombre}";
                labelNombre.Size = new Size(labelWidth, labelHeight);
                labelNombre.Location = new Point(0, i * labelHeight);
                labelNombre.ForeColor = Color.Red;
                labelMap.Add(this.alumnos[i].Id, labelNombre);
                panelListaAlumnos.Controls.Add(labelNombre);

                tableLabels.Add(labelNombre);
            }
        }

        private string[] TokenizeName(Alumno alumno)
        {
            return $"{alumno.PrimerApellido} {alumno.SegundoApellido} {alumno.Nombre}".ToLower().Split(' ');
        }

        private void panelListaAlumnos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Checker_Tick(object sender, EventArgs e)
        {
            string chunk = textExtractor.GetChunk();
            List<string> idsAlumnosEncontrados = new List<string>();
            foreach(KeyValuePair<string, Alumno> entry in alumnosAusentes)
            {
                if (SearchInChunks(nameTokensMap[entry.Key], chunk))
                {
                    idsAlumnosEncontrados.Add(entry.Key);
                }
            }
            foreach(string id in idsAlumnosEncontrados)
            {
                labelMap[id].ForeColor = Color.Green;
                alumnosPresentes.Add(id, alumnosAusentes[id]);
                alumnosAusentes.Remove(id);
            }
        }

        private bool SearchInChunks(string[] tokens, string chunks)
        {
            foreach(string token in tokens)
            {
                if (!chunks.Contains(token))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
