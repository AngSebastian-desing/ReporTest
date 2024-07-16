using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ReporTest.DataSource;
using ReporTest.Model;
using ReporTest.Util;

namespace ReporTest.Service
{
    public class LoginService
    {
        private readonly HttpHelper _httpHelper;

        public LoginService()
        {
            _httpHelper = new HttpHelper();
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            try
            {
                string loginUrl = Urls.urlLogin;

                var requestData = new LoginRequestData(email, password);

                // Realizar la solicitud HTTP POST usando HttpHelper
                var loginResponse = await _httpHelper.SendHttpRequestAsync<LoginResponse>(loginUrl, HttpMethod.Post, requestData);

                if (loginResponse != null && loginResponse.Response != null)
                {
                    return loginResponse.Response;
                }
                else
                {
                    throw new Exception("Respuesta de login inválida");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al realizar el login: {ex.Message}");
            }
        }
    }
}

    