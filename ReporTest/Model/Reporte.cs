﻿using System;

namespace ReporTest.Model
{
    public class Reporte
    {
        public int Folio { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string Estatus { get; set; }
        public string Imagen { get; set; }
        public DateTime Fecha_autorizacion { get; set; }
        public string Usuario_gestiona { get; set; }
        public Usuario User { get; set; }
        
    }
}