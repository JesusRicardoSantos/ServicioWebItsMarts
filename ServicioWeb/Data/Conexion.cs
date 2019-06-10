using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ServicioWeb.Data
{
    public class Conexion
    {
        public object EjecutarProcedimiento(string srtNombreProcedimiento, MySqlParameter[] mySqlParameters, bool blnParametroSalida, List<string> lstCampos)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection("User=a4635a_chuy96; password=acpXgQF8JxSVt2E; server=mysql7003.site4now.net; database=db_a4635a_chuy96"))
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand(srtNombreProcedimiento, mySqlConnection))
                    {
                        IDictionary<string, object> dictionaryData;
                        int iResult = 0;

                        mySqlCommand.CommandType = CommandType.StoredProcedure;
                        mySqlCommand.Parameters.AddRange(mySqlParameters);
                        mySqlConnection.Open();

                        if (blnParametroSalida) //si es true hace el strored procedure tiene parametros de salida
                        {
                            mySqlCommand.ExecuteNonQuery();

                            foreach (MySqlParameter p in mySqlCommand.Parameters)
                                if (p.Direction != ParameterDirection.Input) iResult = int.Parse(p.Value.ToString());

                            return iResult;
                        }
                        else //Regresa un modelo de la base de datos
                        {
                            List<object> lstModeloBase = new List<object>();

                            var mySqlReader = mySqlCommand.ExecuteReader();

                            while (mySqlReader.Read())
                            {
                                dictionaryData = new Dictionary<string, object>();

                                foreach (var item in lstCampos)
                                {
                                    dictionaryData.Add(item, mySqlReader[item]);
                                }

                                lstModeloBase.Add(dictionaryData);
                            }

                            return lstModeloBase;
                        }
                    }
                }
            }
            catch (MySqlException er)
            {
                Console.WriteLine(er.Message);
                return null;
            }
        }
    }
}