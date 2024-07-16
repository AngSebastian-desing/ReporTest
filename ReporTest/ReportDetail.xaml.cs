using ReporTest.Model;
using ReporTest.Util;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReporTest
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReportDetail : ContentPage
	{
        public ReportDetail(Reporte reporte)
        {
            InitializeComponent();

            Lblfolio.Text = reporte.Folio.ToString();
            LblTitulo.Text = reporte.Titulo.ToString();
            LblDescripcion.Text = reporte.Descripcion.ToString();
            LblFecha.Text = reporte.Fecha.ToString();
            LblEstatus.Text = (string)new StatusConverter().Convert(reporte.Estatus, typeof(string), null, null);
            LblFechaAuth.Text = reporte.Fecha_autorizacion.ToString();
            LblUsuarioAtn.Text = reporte.Usuario_gestiona.ToString();

            // Convertir la cadena base64 en un ImageSource
            if (!string.IsNullOrEmpty(reporte.Imagen))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(reporte.Imagen);
                    ImageSource imageSource = ImageSource.FromStream(() => new System.IO.MemoryStream(imageBytes));
                    ImgReporte.Source = imageSource;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al convertir imagen: {ex.Message}");
                }
            }


        }
    }
}