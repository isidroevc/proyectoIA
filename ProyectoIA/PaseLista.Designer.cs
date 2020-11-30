namespace ProyectoIA
{
    partial class PaseLista
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelListaAlumnos = new System.Windows.Forms.Panel();
            this.Checker = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panelListaAlumnos
            // 
            this.panelListaAlumnos.AutoScroll = true;
            this.panelListaAlumnos.BackColor = System.Drawing.SystemColors.Window;
            this.panelListaAlumnos.Location = new System.Drawing.Point(28, 199);
            this.panelListaAlumnos.Name = "panelListaAlumnos";
            this.panelListaAlumnos.Size = new System.Drawing.Size(880, 388);
            this.panelListaAlumnos.TabIndex = 16;
            this.panelListaAlumnos.Paint += new System.Windows.Forms.PaintEventHandler(this.panelListaAlumnos_Paint);
            // 
            // Checker
            // 
            this.Checker.Enabled = true;
            this.Checker.Interval = 1000;
            this.Checker.Tick += new System.EventHandler(this.Checker_Tick);
            // 
            // PaseLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 634);
            this.Controls.Add(this.panelListaAlumnos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PaseLista";
            this.Text = "PaseLista";
            this.Load += new System.EventHandler(this.PaseLista_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelListaAlumnos;
        private System.Windows.Forms.Timer Checker;
    }
}