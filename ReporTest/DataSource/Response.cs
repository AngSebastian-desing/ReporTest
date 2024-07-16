using System;
using System.Collections.Generic;
using System.Text;

namespace ReporTest.DataSource
{
    public class Response
    {
        public string mensaje { get; set; }
        public List<Response> reports { get; set; }
    }

}
