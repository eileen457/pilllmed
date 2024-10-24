using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace ver.Clases
{
    internal class Conexion
    {
        static string server= "EILEEN\\SQLEXPRESS"; 
        static string database = "pillmed";
        static string puerto ="1433";
        private string cadenaConexion;



        public Conexion()
        {
           // cadenaConexion = "Data Source" + server + "," + puerto + ";" + "Initial Catalog" + database + ";" +
           //     "Integrated Security=true";
            cadenaConexion = "Data Source=" + server + "," + puerto + ";" +
                        "Initial Catalog=" + database + ";" +
                        "Integrated Security=true;";
        }

        public SqlConnection getConexion()  
        {
            SqlConnection conexion = null;
            try
            {

                //objeto
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                //  MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                // return conexion;

            }
            catch (SqlException ex)
            {

                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
            finally { conexion.Close(); }
            return conexion;
        }

       

    }
}
