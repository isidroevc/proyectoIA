using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ProyectoIA.Data
{
    public partial class CrearGrupo : Form
    {
        private Form parent;
        private List<Alumno> alumnos;
        private List<Label> tableLabels;
        List<Button> tableButtons;
        private int labelWidth;
        private int labelHeight;
        public CrearGrupo()
        {
            InitializeComponent();
        }

        public CrearGrupo(Form parent)
        {
            this.parent = parent;
            this.alumnos = new List<Alumno>();
            this.tableLabels = new List<Label>();
            this.tableButtons = new List<Button>();
            this.labelWidth = 780;
            this.labelHeight = 24;
            InitializeComponent();
        }

        private void CrearGrupo_Load(object sender, EventArgs e)
        {
            
        }

        private void refrescarListaAlumnos()
        {
            tableLabels.ForEach(label => label.Dispose());
            tableLabels.Clear();

            tableButtons.ForEach(button => button.Dispose());
            tableButtons.Clear();

            for(int i = 0, count = this.alumnos.Count; i < count; i++)
            {
                Label labelNombre = new Label();
                labelNombre.Text = $"{alumnos[i].PrimerApellido} {alumnos[i].SegundoApellido} {alumnos[i].Nombre}";
                labelNombre.Size = new Size(labelWidth, labelHeight);
                labelNombre.Location = new Point(0, i * labelHeight);

                Button deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.Size = new Size(90, labelHeight);
                deleteButton.Name = $"{i}";
                deleteButton.Click += (object sender, EventArgs e) =>
                {
                    Button senderButton = (Button)sender;
                    int index = Convert.ToInt32(senderButton.Name);
                    alumnos.RemoveAt(index);
                    refrescarListaAlumnos();
                };
                deleteButton.Location = new Point(780, i * labelHeight);

                panelListaAlumnos.Controls.Add(labelNombre);
                panelListaAlumnos.Controls.Add(deleteButton);

                tableLabels.Add(labelNombre);
                tableButtons.Add(deleteButton);
            }


        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            Alumno alumno = new Alumno() {
                NumeroControl = txtNumeroControl.Text.Trim(),
                Nombre = txtNombreAlumno.Text.Trim(),
                PrimerApellido = txtPrimerApellidoAlumno.Text.Trim(),
                SegundoApellido  = txtSegundoApellidoAlumno.Text.Trim()
            };

            if (!(validarNumeroControl(alumno.NumeroControl)
                && validarString(alumno.Nombre)
                && validarString(alumno.PrimerApellido)
                ))
            {
                MessageBox.Show("Todos los campos del alumno son obligatorios");
                return;
            }
            alumnos.Add(alumno);
            txtNumeroControl.Text = "";
            txtNombreAlumno.Text = "";
            txtPrimerApellidoAlumno.Text = "";
            txtSegundoApellidoAlumno.Text = "";
            refrescarListaAlumnos();
        }

        private bool validarString(string s)
        {
            return s.Length > 0;
        }

        private bool validarNumeroControl(string s)
        {
            try
            {
                int parsedNumber = Convert.ToInt32(s);
                return s.Length == 8;
            } catch(Exception ex)
            {
                return false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validarNumeroGrupo(string s)
        {
            try
            {
                int parsedNumber = Convert.ToInt32(s);
                return s.Length == 4;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void btnTerminarCreacion_Click(object sender, EventArgs e)
        {
            RepositorioAlumno repositorioAlumno = new RepositorioAlumno();
            RepositorioGrupo repositorioGrupo = new RepositorioGrupo();
            Grupo grupo = new Grupo()
            {
                Nombre = txtNombreGrupo.Text,
                Numero = txtNumeroGrupo.Text
            };
            grupo = repositorioGrupo.Crear(grupo);
            foreach(Alumno alumno in alumnos)
            {
                alumno.IdGrupo = grupo.Id;
                repositorioAlumno.Crear(alumno);
            }
            if (!(validarString(grupo.Nombre) && validarNumeroGrupo(grupo.Numero))) {
                MessageBox.Show("Todos los datos del grupo son obligarios");
            }
            MessageBox.Show("Grupo creado con éxito");
            this.Close();
            this.Dispose();
        }
    }
}
