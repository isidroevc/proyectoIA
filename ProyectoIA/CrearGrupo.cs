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
        private Grupo grupo;
        private List<Alumno> alumnos;
        List<string> alumnosABorrar;
        private RepositorioAlumno repositorioAlumno;
        private RepositorioGrupo repositorioGrupo;
        private string modo;
        private string[] columnasDeDatos;
        public CrearGrupo()
        {
            modo = "creacion";
            alumnos = new List<Alumno>();
            InicializarRepositorios();
            InitializeComponent();
        }

        public CrearGrupo(Grupo grupo)
        {
            this.grupo = grupo;
            alumnosABorrar = new List<string>();
            InicializarRepositorios();
            alumnos = repositorioAlumno.ObtenerAlumnosDeUnGrupo(grupo.Id);
            modo = "edicion";
            InitializeComponent();
            txtNumeroGrupo.Text = grupo.Numero;
            txtNombreGrupo.Text = grupo.Nombre;
            refrescarListaAlumnosParaEdicion();
        }

        private void InicializarRepositorios()
        {
            repositorioGrupo = new RepositorioGrupo();
            repositorioAlumno = new RepositorioAlumno();
        }

        private void CrearGrupo_Load(object sender, EventArgs e)
        {
            
        }

        private void refrescarListaAlumnosParaCreacion()
        {
            tablaAumnos.Columns.Clear();
            tablaAumnos.ReadOnly = true;
            tablaAumnos.Rows.Clear();
            tablaAumnos.Refresh();
            columnasDeDatos = new string[] { "Numero de control", "Primer Apellido", "Segundo Apellido", "Nombre" };
            for(int i = 0, c = columnasDeDatos.Length; i< c; i++)
            {
                tablaAumnos.Columns.Add(columnasDeDatos[i], columnasDeDatos[i]);
            }
            DataGridViewButtonColumn columnaEliminar = new DataGridViewButtonColumn();
            columnaEliminar.HeaderText = "Eliminar";
            columnaEliminar.Text = "Eliminar";
            columnaEliminar.UseColumnTextForButtonValue = true;
            tablaAumnos.Columns.Add(columnaEliminar);
            foreach (Alumno alumno in alumnos)
            {
                string[] row = { alumno.NumeroControl, alumno.PrimerApellido, alumno.SegundoApellido, alumno.Nombre };
                tablaAumnos.Rows.Add(row);
            }
        }

        private void refrescarListaAlumnosParaEdicion()
        {
            tablaAumnos.Columns.Clear();
            tablaAumnos.Rows.Clear();
            tablaAumnos.Refresh();
            columnasDeDatos = new string[] { "Numero de control", "Primer Apellido", "Segundo Apellido", "Nombre" };
            for (int i = 0, c = columnasDeDatos.Length; i < c; i++)
            {
                tablaAumnos.Columns.Add(columnasDeDatos[i], columnasDeDatos[i]);
            }
            DataGridViewButtonColumn columnaEliminar = new DataGridViewButtonColumn();
            columnaEliminar.HeaderText = "Eliminar";
            columnaEliminar.Text = "Eliminar";
            columnaEliminar.UseColumnTextForButtonValue = true;
            tablaAumnos.Columns.Add(columnaEliminar);
            foreach (Alumno alumno in alumnos)
            {
                string[] row = { alumno.NumeroControl, alumno.PrimerApellido, alumno.SegundoApellido, alumno.Nombre };
                tablaAumnos.Rows.Add(row);
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
            refrescarListaAlumnosParaCreacion();
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
            if (modo == "creacion")
            {
                crearGrupo();
            } else if (modo == "edicion")
            {
                actualizarGrupo();
            }
        }

        private void actualizarGrupo()
        {
   
            if (!(validarString(txtNombreGrupo.Text) && validarNumeroGrupo(txtNumeroGrupo.Text)))
            {
                MessageBox.Show("Todos los datos del grupo son obligarios");
                return;
            }
            grupo.Nombre = txtNombreGrupo.Text;
            grupo.Numero = txtNumeroGrupo.Text;
            grupo = repositorioGrupo.ActualizarGrupo(grupo);
            foreach (Alumno alumno in alumnos)
            {
                if (alumno.Id != null && alumno.Id != "")
                {
                    repositorioAlumno.Actualizar(alumno);
                } else
                {
                    alumno.IdGrupo = grupo.Id;
                    repositorioAlumno.Crear(alumno);
                }
                
            }
            foreach(string alumnoABorrar in alumnosABorrar)
            {
                repositorioAlumno.Borrar(alumnoABorrar);
            }
            MessageBox.Show("Grupo creado con éxito");
            this.Close();
            this.Dispose();
        }

        private void crearGrupo()
        {
            grupo = new Grupo()
            {
                Nombre = txtNombreGrupo.Text,
                Numero = txtNumeroGrupo.Text
            };
            if (!(validarString(grupo.Nombre) && validarNumeroGrupo(grupo.Numero)))
            {
                MessageBox.Show("Todos los datos del grupo son obligarios");
                return;
            }
            grupo = repositorioGrupo.Crear(grupo);
            foreach (Alumno alumno in alumnos)
            {
                alumno.IdGrupo = grupo.Id;
                repositorioAlumno.Crear(alumno);
            }
            MessageBox.Show("Grupo creado con éxito");
            this.Close();
            this.Dispose();
        }

        private void tablaAumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && this.alumnos.Count -1 >= e.RowIndex)
            {
                if (modo == "creacion")
                {
                    alumnos.RemoveAt(e.RowIndex);
                    refrescarListaAlumnosParaCreacion();
                } else if (modo == "edicion")
                {
                    DialogResult confirmationResult = MessageBox.Show("¿Estas seguro de eliminar este alumno?\nAdvertencia: No se podrá revertir este paso",
                        "Confirmar eliminacion",
                    MessageBoxButtons.YesNo);
                    if (confirmationResult == DialogResult.Yes)
                    {
                        alumnosABorrar.Add(alumnos[e.RowIndex].Id);
                        alumnos.RemoveAt(e.RowIndex);
                        refrescarListaAlumnosParaEdicion();
                    }
                }
            }
        }

        private void tablaAumnos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= columnasDeDatos.Length)
            {
                return;
            }
            string columna = columnasDeDatos[e.ColumnIndex];
            string value = tablaAumnos.Rows[e.RowIndex].Cells[columna].Value.ToString();
            if (columna == "Numero de control")
            {
                if (!validarNumeroControl(value))
                {
                    MessageBox.Show("Introduzca un número de control válido");
                    tablaAumnos.Rows[e.RowIndex].Cells[columna].Value = alumnos[e.RowIndex].NumeroControl;
                } else
                {
                    alumnos[e.RowIndex].NumeroControl = value;
                }
            }

            if (columna == "Primer Apellido")
            {
                if (!validarString(value))
                {
                    MessageBox.Show("Introduzca un apellido válido");
                    tablaAumnos.Rows[e.RowIndex].Cells[columna].Value = alumnos[e.RowIndex].PrimerApellido;
                }
                else
                {
                    alumnos[e.RowIndex].PrimerApellido = value;
                }
            }

            if (columna == "Segundo Apellido")
            {
                if (!validarString(value))
                {
                    MessageBox.Show("Introduzca un apellido válido");
                    tablaAumnos.Rows[e.RowIndex].Cells[columna].Value = alumnos[e.RowIndex].SegundoApellido;
                }
                else
                {
                    alumnos[e.RowIndex].SegundoApellido = value;
                }
            }


            if (columna == "Nombre")
            {
                if (!validarString(value))
                {
                    MessageBox.Show("Introduzca un nombre válido");
                    tablaAumnos.Rows[e.RowIndex].Cells[columna].Value = alumnos[e.RowIndex].Nombre;
                }
                else
                {
                    alumnos[e.RowIndex].Nombre = value;
                }
            }
        }
    }
}
