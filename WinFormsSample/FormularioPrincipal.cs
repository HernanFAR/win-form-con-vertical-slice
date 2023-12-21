using Dominio.Funcional.Resultados;
using FluentValidation;
using Logica.Funcionalidades.Preguntas.ActualizarPregunta;
using Logica.Funcionalidades.Preguntas.CrearPregunta;
using Logica.Funcionalidades.Preguntas.LeerPreguntas;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica.Funcionalidades.Preguntas.BorrarPregunta;
using WinFormsSample.Herramientas;

namespace WinFormsSample
{
    public partial class FormularioPrincipal : Form
    {
        public static readonly IServiceProvider Proveedor = IniciadorServiceProvider.ProveedorLazy.Value;

        private readonly ISender _sender;
        private readonly CrearPreguntaComando _crearPreguntaComando;
        private ActualizarPreguntaComando _actualizarPreguntaComando;

        private EModo _modo = EModo.Creacion;
        private bool _formularioEditado = true;
        private PreguntaDTO[] _preguntas;

        public FormularioPrincipal()
        {
            InitializeComponent();

            _sender = Proveedor.GetRequiredService<ISender>();
            _crearPreguntaComando = new CrearPreguntaComando();
        }

        private async void ValidarPregunta(object sender, EventArgs e)
        {
            switch (_modo)
            {
                case EModo.Creacion when _formularioEditado:
                    await ValidarModoCreacion();
                    return;
                case EModo.Edicion when _formularioEditado:
                    await ValidarModoEdicion();
                    return;
                default:
                    _formularioEditado = true;
                    break;
            }
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

        private async Task ValidarModoEdicion()
        {
            _actualizarPreguntaComando.Titulo = tituloTextBox.Text;
            _actualizarPreguntaComando.Detalle = detalleRichTextBox.Text;

            var validador = Proveedor.GetRequiredService<IValidator<ActualizarPreguntaComando>>();
            var validacion = await validador.ValidateAsync(_actualizarPreguntaComando);

            tituloLabelValidacion.Text = string.Empty;
            detalleValidacionLabel.Text = string.Empty;
            editarBoton.Enabled = true;

            if (validacion.IsValid)
            {
                return;
            }

            validacion.Errors
                .ForEach(f =>
                {
                    switch (f.PropertyName)
                    {
                        case nameof(ActualizarPreguntaComando.Titulo):
                            tituloLabelValidacion.Text += f.ErrorMessage + Environment.NewLine;
                            break;
                        case nameof(ActualizarPreguntaComando.Detalle):
                            detalleValidacionLabel.Text += f.ErrorMessage + Environment.NewLine;
                            break;
                    }
                });

            tituloLabelValidacion.Visible = tituloLabelValidacion.Text != string.Empty;
            detalleValidacionLabel.Visible = detalleValidacionLabel.Text != string.Empty;

            editarBoton.Enabled = !(tituloLabelValidacion.Visible || detalleValidacionLabel.Visible);
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
                    BorrarPregunta(e.RowIndex);
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
                var textoResolver = pregunta.Resuelta ? "Ya resuelta" : "Resolver";

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
            _modo = EModo.Creacion;

            tituloTextBox.Text = "";
            detalleRichTextBox.Text = "";

            crearBoton.Visible = true;

            editarBoton.Visible = false;
            volverBoton.Visible = false;
            tituloLabelValidacion.Text = string.Empty;
            detalleValidacionLabel.Text = string.Empty;

            _formularioEditado = false;
        }

        private void DetallarPregunta(int rowId)
        {
            var pregunta = _preguntas[rowId];

            _modo = EModo.Edicion;

            crearBoton.Visible = false;

            editarBoton.Visible = true;
            volverBoton.Visible = true;

            _actualizarPreguntaComando = new ActualizarPreguntaComando(pregunta.Id)
            {
                Titulo = pregunta.Titulo,
                Detalle = pregunta.Detalle
            };

            tituloTextBox.Text = pregunta.Titulo;
            detalleRichTextBox.Text = pregunta.Detalle;
            tituloLabelValidacion.Text = string.Empty;
            detalleValidacionLabel.Text = string.Empty;

            _formularioEditado = false;
        }

        private async void EditarPregunta(object sender_, EventArgs e)
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

            var result = MessageBox.Show(
                "¿Seguro que quieres borrar esta pregunta?",
                "Confirmación",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Cancel)
            {
                return;
            }

            var respuesta = await _sender.Send(_actualizarPreguntaComando);

            if (respuesta.Exito)
            {
                MessageBox.Show(
                    "Se actualizó correctamente la pregunta",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                await CargarPreguntas();

                return;
            }

            MessageBox.Show(
                respuesta.ErrorDeNegocio.Mensaje,
                "Error actualizando pregunta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            if (respuesta.ErrorDeNegocio.Tipo == TipoDeError.ErrorDeValidation)
            {
                await ValidarModoEdicion();
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

            var result = MessageBox.Show(
                "¿Seguro que quieres borrar esta pregunta?",
                "Confirmación",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Cancel)
            {
                return;
            }

            var respuesta = await _sender.Send(_crearPreguntaComando);

            if (respuesta.Exito)
            {
                MessageBox.Show(
                    "Se creo correctamente la pregunta",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

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

        private async Task BorrarPregunta(int rowId)
        {
            var result = MessageBox.Show(
                "¿Seguro que quieres borrar esta pregunta?",
                "Confirmación",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
            );

            if (result == DialogResult.Cancel)
            {
                return;
            }

            var pregunta = _preguntas[rowId];

            var respuesta = await _sender.Send(new BorrarPreguntaComando(pregunta.Id));

            if (respuesta.Exito)
            {
                MessageBox.Show(
                    "Se borro correctamente la pregunta",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                await CargarPreguntas();

                return;
            }

            MessageBox.Show(
                respuesta.ErrorDeNegocio.Mensaje,
                "Error borrando pregunta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
