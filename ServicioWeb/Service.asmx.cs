using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using ServicioWeb.Data;
using ServicioWeb.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

namespace ServicioWeb
{
    /// <summary>
    /// Descripción breve de Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private List<string> lstCampos = new List<string>() { "Nombre", "A_paterno", "A_materno", "Telefono", "Cliente_sap", "Fecha_creacion", "Nombre_fiscal", "Rfc", "Contacto", "Cedild", "Longitud", "Latitud", "Contrasena", "Referencia", "Foto_local" };
        private MySqlParameter[] mySqlParameter;

        [WebMethod]
        public string test(string strNombre)
        {
            return "Hola " + strNombre + " \n:D";
        }

        [WebMethod]
        public string Login(string strBody)
        {
            try
            {
                var jsonBody = JsonConvert.DeserializeObject<Usuario>(strBody);
                jsonBody.Contrasena = HashSHA1(jsonBody.Contrasena);

                mySqlParameter = new MySqlParameter[2];
                mySqlParameter[0] = new MySqlParameter("iCliente_sap", jsonBody.Cliente_sap);
                mySqlParameter[1] = new MySqlParameter("iContrasena", jsonBody.Contrasena);

                var con = new Conexion();

                return JsonConvert.SerializeObject(con.EjecutarProcedimiento("p_Sesion", mySqlParameter, false, lstCampos));
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        public int CrearCuenta(string strBody)
        {
            try
            {
                var jsonBody = JsonConvert.DeserializeObject<Usuario>(strBody);
                jsonBody.Contrasena = HashSHA1(jsonBody.Contrasena);

                mySqlParameter = new MySqlParameter[16];
                mySqlParameter[0] = new MySqlParameter("iNombre", jsonBody.Nombre);
                mySqlParameter[1] = new MySqlParameter("iA_paterno", jsonBody.A_paterno);
                mySqlParameter[2] = new MySqlParameter("iA_materno", jsonBody.A_materno);
                mySqlParameter[3] = new MySqlParameter("iTelefono", jsonBody.Telefono);
                mySqlParameter[4] = new MySqlParameter("iCliente_sap", jsonBody.Cliente_sap);
                mySqlParameter[5] = new MySqlParameter("iFecha_creacion", jsonBody.Fecha_creacion);
                mySqlParameter[6] = new MySqlParameter("iNombre_fiscal", jsonBody.Nombre_fiscal);
                mySqlParameter[7] = new MySqlParameter("iRfc", jsonBody.Rfc);
                mySqlParameter[8] = new MySqlParameter("iContacto", jsonBody.Contacto);
                mySqlParameter[9] = new MySqlParameter("iCedild", jsonBody.Cedild);
                mySqlParameter[10] = new MySqlParameter("iLongitud", jsonBody.Longitud);
                mySqlParameter[11] = new MySqlParameter("iLatitud", jsonBody.Latitud);
                mySqlParameter[12] = new MySqlParameter("iContrasena", jsonBody.Contrasena);
                mySqlParameter[13] = new MySqlParameter("iReferencia", jsonBody.Referencia);
                mySqlParameter[14] = new MySqlParameter("iFoto_local", jsonBody.Foto_local);
                mySqlParameter[15] = new MySqlParameter("salida", "") { Direction = ParameterDirection.Output };

                var con = new Conexion();
                return int.Parse(con.EjecutarProcedimiento("p_AgregarCliente", mySqlParameter, true, null).ToString());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 5; //Error en el WebService
            }
        }

        [WebMethod]
        public int EliminarCuenta(int intCliente)
        {
            try
            {
                mySqlParameter = new MySqlParameter[2];
                mySqlParameter[0] = new MySqlParameter("iCliente_sap", intCliente);
                mySqlParameter[1] = new MySqlParameter("salida", "") { Direction = ParameterDirection.Output };
                var con = new Conexion();
                return int.Parse(con.EjecutarProcedimiento("p_EliminarCliente", mySqlParameter, true, null).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 5;
            }
        }

        [WebMethod]
        public int ActualizarCuenta(string strBody)
        {
            try
            {
                var jsonBody = JsonConvert.DeserializeObject<Usuario>(strBody);
                jsonBody.Contrasena = HashSHA1(jsonBody.Contrasena);

                mySqlParameter = new MySqlParameter[16];
                mySqlParameter[0] = new MySqlParameter("iNombre", jsonBody.Nombre);
                mySqlParameter[1] = new MySqlParameter("iA_paterno", jsonBody.A_paterno);
                mySqlParameter[2] = new MySqlParameter("iA_materno", jsonBody.A_materno);
                mySqlParameter[3] = new MySqlParameter("iTelefono", jsonBody.Telefono);
                mySqlParameter[4] = new MySqlParameter("iCliente_sap", jsonBody.Cliente_sap);
                mySqlParameter[5] = new MySqlParameter("iFecha_creacion", jsonBody.Fecha_creacion);
                mySqlParameter[6] = new MySqlParameter("iNombre_fiscal", jsonBody.Nombre_fiscal);
                mySqlParameter[7] = new MySqlParameter("iRfc", jsonBody.Rfc);
                mySqlParameter[8] = new MySqlParameter("iContacto", jsonBody.Contacto);
                mySqlParameter[9] = new MySqlParameter("iCedild", jsonBody.Cedild);
                mySqlParameter[10] = new MySqlParameter("iLongitud", jsonBody.Longitud);
                mySqlParameter[11] = new MySqlParameter("iLatitud", jsonBody.Latitud);
                mySqlParameter[12] = new MySqlParameter("iContrasena", jsonBody.Contrasena);
                mySqlParameter[13] = new MySqlParameter("iReferencia", jsonBody.Referencia);
                mySqlParameter[14] = new MySqlParameter("iFoto_local", jsonBody.Foto_local);
                mySqlParameter[15] = new MySqlParameter("salida", "") { Direction = ParameterDirection.Output };

                var con = new Conexion();
                return int.Parse(con.EjecutarProcedimiento("p_ActulizarCliente ", mySqlParameter, true, null).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 5;
            }
        }

        [WebMethod]
        public string VisualizarCuenta(int intCliente, int intOpcion)
        {
            try
            {
                mySqlParameter = new MySqlParameter[2];
                mySqlParameter[0] = new MySqlParameter("iCliente_sap", intCliente);
                mySqlParameter[1] = new MySqlParameter("opcion", intOpcion);

                var con = new Conexion();

                if (intOpcion != 2)
                {
                    lstCampos = new List<string>();

                    lstCampos.Add("Cliente_sap");
                    lstCampos.Add("Nombre");
                    lstCampos.Add("A_paterno");
                    lstCampos.Add("A_materno");
                    lstCampos.Add("Cedild");

                    return JsonConvert.SerializeObject(con.EjecutarProcedimiento("p_VerClientes ", mySqlParameter, false, lstCampos));
                }
                else
                    return JsonConvert.SerializeObject(con.EjecutarProcedimiento("p_VerClientes ", mySqlParameter, false, lstCampos));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string HashSHA1(string value)
        {
            var sha1 = SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
