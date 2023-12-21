namespace WinFormsSample
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.principalLabel = new System.Windows.Forms.Label();
            this.tituloLabel = new System.Windows.Forms.Label();
            this.tituloTextBox = new System.Windows.Forms.TextBox();
            this.detalleLabel = new System.Windows.Forms.Label();
            this.detalleRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tituloLabelValidacion = new System.Windows.Forms.Label();
            this.detalleValidacionLabel = new System.Windows.Forms.Label();
            this.preguntasGrid = new System.Windows.Forms.DataGridView();
            this.Identificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Titulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detallar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Borrar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Resolver = new System.Windows.Forms.DataGridViewButtonColumn();
            this.crearBoton = new System.Windows.Forms.Button();
            this.volverBoton = new System.Windows.Forms.Button();
            this.editarBoton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.preguntasGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // principalLabel
            // 
            this.principalLabel.AutoSize = true;
            this.principalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.principalLabel.Location = new System.Drawing.Point(278, 9);
            this.principalLabel.Name = "principalLabel";
            this.principalLabel.Size = new System.Drawing.Size(251, 29);
            this.principalLabel.TabIndex = 0;
            this.principalLabel.Text = "Preguntas del sistema";
            // 
            // tituloLabel
            // 
            this.tituloLabel.AutoSize = true;
            this.tituloLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloLabel.Location = new System.Drawing.Point(27, 51);
            this.tituloLabel.Name = "tituloLabel";
            this.tituloLabel.Size = new System.Drawing.Size(47, 20);
            this.tituloLabel.TabIndex = 1;
            this.tituloLabel.Text = "Titulo";
            // 
            // tituloTextBox
            // 
            this.tituloTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloTextBox.Location = new System.Drawing.Point(31, 75);
            this.tituloTextBox.Name = "tituloTextBox";
            this.tituloTextBox.Size = new System.Drawing.Size(635, 23);
            this.tituloTextBox.TabIndex = 2;
            this.tituloTextBox.TextChanged += new System.EventHandler(this.ValidarPregunta);
            // 
            // detalleLabel
            // 
            this.detalleLabel.AutoSize = true;
            this.detalleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detalleLabel.Location = new System.Drawing.Point(27, 133);
            this.detalleLabel.Name = "detalleLabel";
            this.detalleLabel.Size = new System.Drawing.Size(59, 20);
            this.detalleLabel.TabIndex = 3;
            this.detalleLabel.Text = "Detalle";
            // 
            // detalleRichTextBox
            // 
            this.detalleRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detalleRichTextBox.Location = new System.Drawing.Point(31, 157);
            this.detalleRichTextBox.Name = "detalleRichTextBox";
            this.detalleRichTextBox.Size = new System.Drawing.Size(635, 96);
            this.detalleRichTextBox.TabIndex = 4;
            this.detalleRichTextBox.Text = "";
            this.detalleRichTextBox.TextChanged += new System.EventHandler(this.ValidarPregunta);
            // 
            // tituloLabelValidacion
            // 
            this.tituloLabelValidacion.AutoSize = true;
            this.tituloLabelValidacion.ForeColor = System.Drawing.Color.Red;
            this.tituloLabelValidacion.Location = new System.Drawing.Point(34, 104);
            this.tituloLabelValidacion.Name = "tituloLabelValidacion";
            this.tituloLabelValidacion.Size = new System.Drawing.Size(38, 13);
            this.tituloLabelValidacion.TabIndex = 5;
            this.tituloLabelValidacion.Text = "Oculta";
            this.tituloLabelValidacion.Visible = false;
            // 
            // detalleValidacionLabel
            // 
            this.detalleValidacionLabel.AutoSize = true;
            this.detalleValidacionLabel.ForeColor = System.Drawing.Color.Red;
            this.detalleValidacionLabel.Location = new System.Drawing.Point(34, 256);
            this.detalleValidacionLabel.Name = "detalleValidacionLabel";
            this.detalleValidacionLabel.Size = new System.Drawing.Size(38, 13);
            this.detalleValidacionLabel.TabIndex = 6;
            this.detalleValidacionLabel.Text = "Oculta";
            this.detalleValidacionLabel.Visible = false;
            // 
            // preguntasGrid
            // 
            this.preguntasGrid.AllowUserToAddRows = false;
            this.preguntasGrid.AllowUserToDeleteRows = false;
            this.preguntasGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.preguntasGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Identificador,
            this.Titulo,
            this.Detalle,
            this.Detallar,
            this.Borrar,
            this.Resolver});
            this.preguntasGrid.Location = new System.Drawing.Point(12, 288);
            this.preguntasGrid.Name = "preguntasGrid";
            this.preguntasGrid.ReadOnly = true;
            this.preguntasGrid.Size = new System.Drawing.Size(776, 150);
            this.preguntasGrid.TabIndex = 7;
            this.preguntasGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClickEnPreguntasGrid);
            // 
            // Identificador
            // 
            this.Identificador.HeaderText = "Identificador";
            this.Identificador.Name = "Identificador";
            this.Identificador.ReadOnly = true;
            this.Identificador.Visible = false;
            this.Identificador.Width = 5;
            // 
            // Titulo
            // 
            this.Titulo.HeaderText = "Título";
            this.Titulo.Name = "Titulo";
            this.Titulo.ReadOnly = true;
            this.Titulo.Width = 150;
            // 
            // Detalle
            // 
            this.Detalle.HeaderText = "Detalle";
            this.Detalle.Name = "Detalle";
            this.Detalle.ReadOnly = true;
            this.Detalle.Width = 350;
            // 
            // Detallar
            // 
            this.Detallar.HeaderText = "Detallar";
            this.Detallar.Name = "Detallar";
            this.Detallar.ReadOnly = true;
            this.Detallar.Text = "¡Clic!";
            this.Detallar.Width = 75;
            // 
            // Borrar
            // 
            this.Borrar.HeaderText = "Borrar";
            this.Borrar.Name = "Borrar";
            this.Borrar.ReadOnly = true;
            this.Borrar.Text = "¡Clic!";
            this.Borrar.Width = 75;
            // 
            // Resolver
            // 
            this.Resolver.HeaderText = "Resolver";
            this.Resolver.Name = "Resolver";
            this.Resolver.ReadOnly = true;
            this.Resolver.Width = 75;
            // 
            // crearBoton
            // 
            this.crearBoton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crearBoton.Location = new System.Drawing.Point(689, 121);
            this.crearBoton.Name = "crearBoton";
            this.crearBoton.Size = new System.Drawing.Size(75, 89);
            this.crearBoton.TabIndex = 8;
            this.crearBoton.Text = "Crear";
            this.crearBoton.UseVisualStyleBackColor = true;
            this.crearBoton.Click += new System.EventHandler(this.CrearPregunta);
            // 
            // volverBoton
            // 
            this.volverBoton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volverBoton.Location = new System.Drawing.Point(689, 75);
            this.volverBoton.Name = "volverBoton";
            this.volverBoton.Size = new System.Drawing.Size(75, 78);
            this.volverBoton.TabIndex = 9;
            this.volverBoton.Text = "Volver";
            this.volverBoton.UseVisualStyleBackColor = true;
            this.volverBoton.Visible = false;
            this.volverBoton.Click += new System.EventHandler(this.Volver);
            // 
            // editarBoton
            // 
            this.editarBoton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editarBoton.Location = new System.Drawing.Point(689, 177);
            this.editarBoton.Name = "editarBoton";
            this.editarBoton.Size = new System.Drawing.Size(75, 76);
            this.editarBoton.TabIndex = 10;
            this.editarBoton.Text = "Editar";
            this.editarBoton.UseVisualStyleBackColor = true;
            this.editarBoton.Visible = false;
            this.editarBoton.Click += new System.EventHandler(this.EditarPregunta);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.editarBoton);
            this.Controls.Add(this.volverBoton);
            this.Controls.Add(this.crearBoton);
            this.Controls.Add(this.preguntasGrid);
            this.Controls.Add(this.detalleValidacionLabel);
            this.Controls.Add(this.tituloLabelValidacion);
            this.Controls.Add(this.detalleRichTextBox);
            this.Controls.Add(this.detalleLabel);
            this.Controls.Add(this.tituloTextBox);
            this.Controls.Add(this.tituloLabel);
            this.Controls.Add(this.principalLabel);
            this.Name = "Form1";
            this.Text = "Preguntas";
            this.Load += new System.EventHandler(this.CargarVistaFormulario);
            ((System.ComponentModel.ISupportInitialize)(this.preguntasGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label principalLabel;
        private System.Windows.Forms.Label tituloLabel;
        private System.Windows.Forms.TextBox tituloTextBox;
        private System.Windows.Forms.Label detalleLabel;
        private System.Windows.Forms.RichTextBox detalleRichTextBox;
        private System.Windows.Forms.Label tituloLabelValidacion;
        private System.Windows.Forms.Label detalleValidacionLabel;
        private System.Windows.Forms.DataGridView preguntasGrid;
        private System.Windows.Forms.Button crearBoton;
        private System.Windows.Forms.Button volverBoton;
        private System.Windows.Forms.Button editarBoton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Identificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn Titulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle;
        private System.Windows.Forms.DataGridViewButtonColumn Detallar;
        private System.Windows.Forms.DataGridViewButtonColumn Borrar;
        private System.Windows.Forms.DataGridViewButtonColumn Resolver;
    }
}

