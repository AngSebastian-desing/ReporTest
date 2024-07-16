using Plugin.Media.Abstractions;
using ReporTest.Util;
using ReporTest.Model;
using System;
using ReporTest.Util;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReporTest.Service;
using Plugin.Media;
using System.Threading.Tasks;

namespace ReporTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateReport : TabbedPage
    {
        private Reporte _reporte;
        private Usuario _usuario;
        private readonly HttpHelper _httpHelper;

        public CreateReport(Usuario usuario)
        {
            InitializeComponent();
            _reporte = new Reporte();
            _usuario = usuario;
            _httpHelper = new HttpHelper();
        }

        private async void TakePhotoButton_Clicked(object sender, EventArgs e)
        {
            await TakePhotoAsync();
        }

        private async Task TakePhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sin cámara", "La cámara no está disponible.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium, // Redimensionar la foto
                CompressionQuality = 75, // Ajusta la calidad de compresión
                Directory = "Temp", // Valor genérico
                Name = $"{DateTime.UtcNow}.jpg"
            });

            if (file == null)
                return;

            showResult.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            showResult.Opacity = 1;

            _reporte.Imagen = ImageConvert.ConvertToBase64(file.GetStream());

            // Libera los recursos si ya no necesitas el archivo
            file.Dispose();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            _reporte.Titulo = txtTitulo.Text?.Trim();
            _reporte.Descripcion = txtDescripcion.Text?.Trim();
            _reporte.Fecha = DateTime.Now;
            _reporte.IdUsuario = _usuario.IdUsuario;
            _reporte.Estatus = "P"; //Estatus por defecto
            _reporte.Fecha_autorizacion = new DateTime(1900, 1, 1, 0, 0, 0); //Fecha aut por defecto
            _reporte.Usuario_gestiona = "ASIGNACIÓN PENDIENTE"; //No ha sido atendido por nadie

            if (!CamposValidos())
            {
                await DisplayAlert( "Error" ,"Por favor, complete todos los datos y realice una foto válida.","Aceptar");

                return;
            }


            try
            {
                var customAlert = new CustomAlertPage("Enviando reporte...");
                await Navigation.PushModalAsync(customAlert);
                var response = await _httpHelper.
                    SendHttpRequestAsync<HttpResponseMessage>
                    (
                        Urls.urlReportSave
                        , HttpMethod.Post
                        , _reporte
                    );


                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito"
                                        , "El reporte se envió correctamente."
                                        , "ACEPTAR"
                                       );

                    CleanFields();
                }
                else
                {
                    await DisplayAlert("Error"
                                        , "Hubo un problema al enviar el reporte."
                                        , "ACEPTAR"
                                       );
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error"
                                    , $"Ocurrió un problema al guardar el reporte: {ex.Message}"
                                    , "ACEPTAR"
                                  );
            }
            finally { await Navigation.PopModalAsync(); }
        }

        public void CleanFields()
        {
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            showResult.Opacity = 0.25;
            showResult.Source = "default_img.png";
        }

        private bool CamposValidos()
        {
            if (string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtDescripcion.Text))
            {
                return false;
            }

            var fileImageSource = showResult.Source as FileImageSource;
            if (fileImageSource != null)
            {
                string fileName = fileImageSource.File;
                if (!fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    return false;

                }
            }
            return true;

        }
    }
}