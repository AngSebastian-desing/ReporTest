using System;
using System.Collections.Generic;
using System.Text;

namespace ReporTest.Model
{
    public class LoginRequestData
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginRequestData(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
