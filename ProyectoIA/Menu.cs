using ProyectoIA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIA
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnPasarLista_Click(object sender, EventArgs e)
        {
            ListadoGrupos listado = new ListadoGrupos();
            listado.ShowDialog(this);
        }

        private void btnCrearGrupo_Click(object sender, EventArgs e)
        {
            CrearGrupo crearGrupo = new CrearGrupo(this);
            crearGrupo.ShowDialog(this);
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
