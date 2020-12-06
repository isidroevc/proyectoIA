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
    public partial class ListadoGrupos : Form
    {
        private List<Grupo> grupos;
        private RepositorioGrupo repositorioGrupo;
        private List<Label> labels;
        private List<Button> buttons;
        public ListadoGrupos()
        {
            repositorioGrupo = new RepositorioGrupo();
            labels = new List<Label>();
            buttons = new List<Button>();
            InitializeComponent();
            CargarGrupos();
            
        }

        private void ListadoGrupos_Load(object sender, EventArgs e)
        {

        }

        private void CargarGrupos()
        {
            int anchoBotones = 60, altoBotones = 20, alturaLabel = 20;
            int index = 0;
            panelListadoGrupos.Controls.Clear();
            labels.ForEach(l => l.Dispose());
            buttons.ForEach(b => b.Dispose());
            this.grupos = this.repositorioGrupo.ObtenerLista();
            foreach(Grupo grupo in grupos)
            {
                
                Label labelGrupo = new Label();
                labelGrupo.Text = $"{grupo.Numero} - {grupo.Nombre}";
                labelGrupo.Location = new Point(0, index * alturaLabel);
                labelGrupo.Size = new Size(330, 20);

                Button botonPasarLista = new Button();
                botonPasarLista.Text = "P. Lista";
                botonPasarLista.Name = grupo.Id;
                botonPasarLista.Size = new Size(anchoBotones, altoBotones);
                botonPasarLista.Location = new Point(340, index * altoBotones);
                botonPasarLista.Click += PasarList_ClickHandler;

                Button botonListas = new Button();
                botonListas.Text = "Listas";
                botonListas.Name = grupo.Id;
                botonListas.Size = new Size(anchoBotones, altoBotones);
                botonListas.Location = new Point(410, index * altoBotones);
                botonListas.Click += VerLista_ClickHandler;

                Button botonEditar = new Button();
                botonEditar.Text = "Editar";
                botonEditar.Name = grupo.Id;
                botonEditar.Size = new Size(anchoBotones, altoBotones);
                botonEditar.Location = new Point(480, index * altoBotones);
                botonEditar.Click += Editar_ClickHandler;

                panelListadoGrupos.Controls.Add(labelGrupo);
                panelListadoGrupos.Controls.Add(botonPasarLista);
                panelListadoGrupos.Controls.Add(botonListas);
                panelListadoGrupos.Controls.Add(botonEditar);

                labels.Add(labelGrupo);
                buttons.Add(botonPasarLista);
                buttons.Add(botonListas);
                buttons.Add(botonEditar);
                index++; 
            }
        }
        
        private void PasarList_ClickHandler(Object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            Grupo grupo = this.grupos.Find(g => g.Id == senderButton.Name);
            PaseLista paseLista = new PaseLista(grupo);
            paseLista.ShowDialog(this);
            
        }

        private void Editar_ClickHandler(Object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            Grupo grupo = this.grupos.Find(g => g.Id == senderButton.Name);
            CrearGrupo crearGrupo = new CrearGrupo(grupo);
            WindowState = FormWindowState.Minimized;
            crearGrupo.ShowDialog(this);
            CargarGrupos();
            WindowState = FormWindowState.Normal;
        }

        private void VerLista_ClickHandler(Object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            Grupo grupo = this.grupos.Find(g => g.Id == senderButton.Name);
            VistaSesionesGrupo vista = new VistaSesionesGrupo(grupo);
            this.WindowState = FormWindowState.Minimized;
            vista.ShowDialog();
            this.WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
