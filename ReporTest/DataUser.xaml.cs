using ReporTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReporTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataUser : ContentPage
    {
        private Usuario _usuario;
        public DataUser()
        {
            InitializeComponent();
        }
        public DataUser(Usuario usuario)
        {
            InitializeComponent();
            this._usuario = usuario;
            ShowDataUser();
        }

        public void ShowDataUser()
        {
            LblNombre.Text = this._usuario.Nombre;
            LblApellido.Text = this._usuario.Apellido;
            LblEdad.Text = this._usuario.Edad.ToString();
            LblPuesto.Text = this._usuario.Puesto;
        }

        private void BtnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            // Eliminar el token almacenado
            //Preferences.Remove("AccessToken");

            // Navegar de regreso a la pantalla de inicio de sesión
            App.Current.MainPage = new NavigationPage(new LoginUI());
            //Navigation.PopToRootAsync();
        }

    }
}