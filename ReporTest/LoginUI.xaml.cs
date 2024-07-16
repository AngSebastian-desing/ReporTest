using ReporTest.Model;
using ReporTest.Service;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReporTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUI : ContentPage
    {
        public LoginUI()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void BtnLogin(object sender, EventArgs e)
        {
            string username = txtUserName.Text?.Trim();
            string keypass = txtPass.Text?.Trim();

            if (ValidateFields(username, keypass))
            {
                IsBusy = true;
                var loginService = new LoginService();

                try
                {
                    Usuario usuario = await loginService.LoginAsync(username, keypass);

                    if (usuario != null)
                    {
                        await Navigation.PushAsync(new MainPage(usuario));
                        Navigation.RemovePage(this);

                    }
                    else
                    {
                        await DisplayAlert("Error", "Usuario o contraseña incorrectos", "Cerrar");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
                    await DisplayAlert("Error", "El usuario y/o contraseña no son válidos", "Cerrar");
                }
                finally { IsBusy = false; }
            }else await DisplayAlert("Error", "Por favor ingrese un correo y contraseña válidos", "Aceptar");

        }

        private bool ValidateFields(string username, string keypass)
        {
            return !string.IsNullOrEmpty(username) && IsValidEmail(username) && !string.IsNullOrEmpty(keypass);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            // Expresión regular para validar correo electrónico
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

    }
}