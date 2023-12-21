using System;
using System.Windows.Forms;
using Logica.Funcionalidades.Preguntas.LeerPreguntas;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WinFormsSample.Herramientas;

namespace WinFormsSample
{
    public partial class Form1 : Form
    {
        public static readonly IServiceProvider Proveedor = IniciadorServiceProvider.ProveedorLazy.Value;

        public Form1()
        {
            InitializeComponent();
            
        }

        private async void CargarVistaFormulario(object sender_, EventArgs e)
        {
            var sender = Proveedor.GetRequiredService<ISender>();

            var respuesta = await sender.Send(LeerPreguntasConsulta.Instancia);

            if (respuesta.ConError)
            {
                MessageBox.Show(
                    "Se ha producido un error obteniendo las preguntas",
                    "Error leyendo preguntas",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }
            
            foreach (var pregunta in respuesta.Valor.Preguntas)
            {
                preguntasGrid.Rows.Add(pregunta.Id, pregunta.Titulo, pregunta.Detalle, "Editar", "Borrar", "Resolver");
            }
        }

        private void ClickEnPreguntasGrid(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 3:
                    MessageBox.Show("Botón editar clickeado en: " + e.RowIndex);
                    break;
                case 4:
                    MessageBox.Show("Botón borrar clickeado en: " + e.RowIndex);
                    break;
                case 5:
                    MessageBox.Show("Botón resolver clickeado en: " + e.RowIndex);
                    break;
            }
        }
    }
}
