using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWeb.Modelo
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string A_paterno { get; set; }
        public string A_materno { get; set; }
        public string Telefono { get; set; }
        public int Cliente_sap { get; set; }
        public DateTime Fecha_creacion { get; set; }
        public string Nombre_fiscal { get; set; }
        public string Rfc { get; set; }
        public string Contacto { get; set; }
        public string Cedild { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public string Contrasena { get; set; }
        public string Referencia { get; set; }
        public string Foto_local { get; set; }
    }
}