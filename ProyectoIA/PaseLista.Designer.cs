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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(832, 599);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 37);
            this.label1.TabIndex = 18;
            this.label1.Text = "Pase de lista";
            // 
            // PaseLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 634);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.panelListaAlumnos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PaseLista";
            this.Text = "PaseLista";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PaseLista_FormClosed);
            this.Load += new System.EventHandler(this.PaseLista_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelListaAlumnos;
        private System.Windows.Forms.Timer Checker;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label1;
    }
}