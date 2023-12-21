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

        private async void Form1_Load(object sender_, EventArgs e)
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

            // poblar el preguntasGridView
            foreach (var pregunta in respuesta.Valor.Preguntas)
            {
                preguntasGrid.Rows.Add(pregunta.Id, pregunta.Titulo, pregunta.Detalle, "Editar", "Borrar", "Resolver");
            }
        }
    }
}
