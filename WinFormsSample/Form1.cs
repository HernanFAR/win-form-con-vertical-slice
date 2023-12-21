using Logica.Funcionalidades.Preguntas.LeerPreguntas;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using CorteComun.Funcional.Resultados;
using FluentValidation;
using Logica.Funcionalidades.Preguntas.CrearPregunta;
using WinFormsSample.Herramientas;

namespace WinFormsSample
{
    public partial class Form1 : Form
    {
        public static readonly IServiceProvider Proveedor = IniciadorServiceProvider.ProveedorLazy.Value;

        private readonly ISender _sender;
        private readonly CrearPreguntaComando _crearPreguntaComando;

        private EModo _modo = EModo.Creacion;
        private bool _formularioEditado = true;
        private PreguntaDTO[] _preguntas;
        private PreguntaDTO _pregunta;

        public Form1()
        {
            InitializeComponent();

            _sender = Proveedor.GetRequiredService<ISender>();
            _crearPreguntaComando = new CrearPreguntaComando();
        }

        private async void ValidarPregunta(object sender, EventArgs e)
        {
            if (_modo == EModo.Creacion && _formularioEditado)
            {
                await ValidarModoCreacion();
                return;
            }

            _formularioEditado = true;
        }

        private async Task ValidarModoCreacion()
        {
            _crearPreguntaComando.Titulo = tituloTextBox.Text;
            _crearPreguntaComando.Detalle = detalleRichTextBox.Text;

            var validador = Proveedor.GetRequiredService<IValidator<CrearPreguntaComando>>();
            var validacion = await validador.ValidateAsync(_crearPreguntaComando);

            tituloLabelValidacion.Text = string.Empty;
            detalleValidacionLabel.Text = string.Empty;
            crearBoton.Enabled = true;

            if (validacion.IsValid)
            {
                return;
            }
             
            validacion.Errors
                .ForEach(f =>
                {
                    switch (f.PropertyName)
                    {
                        case nameof(CrearPreguntaComando.Titulo):
                            tituloLabelValidacion.Text += f.ErrorMessage + Environment.NewLine;
                            break;
                        case nameof(CrearPreguntaComando.Detalle):
                            detalleValidacionLabel.Text += f.ErrorMessage + Environment.NewLine;
                            break;
                    }
                });

            tituloLabelValidacion.Visible = tituloLabelValidacion.Text != string.Empty;
            detalleValidacionLabel.Visible = detalleValidacionLabel.Text != string.Empty;

            crearBoton.Enabled = !(tituloLabelValidacion.Visible || detalleValidacionLabel.Visible);
        }
        
        private async void CargarVistaFormulario(object sender_, EventArgs e)
        {
            await CargarPreguntas();
        }

        private async void ClickEnPreguntasGrid(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 3:
                    DetallarPregunta(e.RowIndex);
                    break;
                case 4:
                    MessageBox.Show("Botón borrar clickeado en: " + e.RowIndex);
                    break;
                case 5:
                    MessageBox.Show("Botón resolver clickeado en: " + e.RowIndex);
                    break;
            }
        }

        private async Task CargarPreguntas()
        {
            var respuesta = await _sender.Send(LeerPreguntasConsulta.Instancia);

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

            _preguntas = respuesta.Valor.Preguntas;
            preguntasGrid.Rows.Clear();

            foreach (var pregunta in _preguntas)
            {
                var textoResolver = pregunta.Resuelta ? "Resolver" : "Ya resuelta";

                preguntasGrid.Rows.Add(
                    pregunta.Id,
                    pregunta.Titulo,
                    pregunta.Detalle,
                    "Detallar", "Borrar",
                    textoResolver);
            }
        }

        private void Volver(object sender, EventArgs e)
        {
            ColocarModoCreacion();
        }

        private void ColocarModoEdicion(PreguntaDTO pregunta)
        {
            _modo = EModo.Edicion;
            _pregunta = pregunta;

            crearBoton.Visible = false;

            editarBoton.Visible = true;
            volverBoton.Visible = true;

            tituloTextBox.Text = pregunta.Titulo;
            detalleRichTextBox.Text = pregunta.Detalle;
            tituloLabelValidacion.Text = string.Empty;
            detalleValidacionLabel.Text = string.Empty;
        }

        private void ColocarModoCreacion()
        {
            _modo = EModo.Creacion;
            _pregunta = null;

            tituloTextBox.Text = "";
            detalleRichTextBox.Text = "";

            crearBoton.Visible = true;

            editarBoton.Visible = false;
            volverBoton.Visible = false;
            tituloLabelValidacion.Text = string.Empty;
            detalleValidacionLabel.Text = string.Empty;
        }

        private void DetallarPregunta(int rowId)
        {
            var pregunta = _preguntas[rowId];

            ColocarModoEdicion(pregunta);

            tituloTextBox.Text = pregunta.Titulo;
            detalleRichTextBox.Text = pregunta.Detalle;

            _formularioEditado = false;
        }

        private void EditarPregunta(object sender_, EventArgs e)
        {
            if (_modo == EModo.Creacion)
            {
                MessageBox.Show(
                    "No puedes editar si estas en modo creación",
                    "Operación negada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }
        }

        private async void CrearPregunta(object sender, EventArgs e)
        {
            if (_modo == EModo.Edicion)
            {
                MessageBox.Show(
                    "No puedes crear si estas en modo edición",
                    "Operación negada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            var respuesta = await _sender.Send(_crearPreguntaComando);

            if (respuesta.Exito)
            {
                await CargarPreguntas();

                return;
            }

            MessageBox.Show(
                respuesta.ErrorDeNegocio.Mensaje,
                "Error creando pregunta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            if (respuesta.ErrorDeNegocio.Tipo == TipoDeError.ErrorDeValidation)
            {
                await ValidarModoCreacion();
            }
        }
    }
}
