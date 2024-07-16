using ReporTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using ReporTest.Service;
using ReporTest.Util;


namespace ReporTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsList : TabbedPage
    {
        private Usuario _usuario;
        private readonly HttpHelper _httpHelper;
        public ReportsList(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            _httpHelper = new HttpHelper();
            _ = ShowReports();
        }

        private async Task ShowReports()
        {
            string url = Urls.urlReportList;
            List<Reporte> reportesUsuario = new List<Reporte>();

            try
            {
                Data data = await _httpHelper.SendHttpRequestAsync<Data>(url, HttpMethod.Get);

                // Se filtran los reportes para mostrar solo los del usuario activo
                reportesUsuario.AddRange(data.Response.Where(reporte => reporte.IdUsuario == _usuario.IdUsuario));

                ReportList.ItemsSource = reportesUsuario;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Ocurrió un error al cargar los reportes", "Cerrar");
            }
        }

        private async void ReportsListView_Refresh(object sender, EventArgs e)
        { 
            await ShowReports();
            ReportList.IsRefreshing = false;
        }

        private void ReportList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                ((ListView)sender).SelectedItem = null;
                Navigation.PushModalAsync(new ReportDetail((e.Item as Reporte)));
            }
        }
    }
}