using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ver.Clases;

namespace ver
{                                                     //ALARMA FUNCIONA
                                                      //PENSAR SI PONER BOTON DE VER LUEGO DE PONER EL NOMBRE DE LA PERSONA A RESTARLE MEDICAMENTO
                                                      //FORM1 TIENE ERROR CON FORM5  CREO QUE ES MEJOR LLAMARLO VACIO Y PONER EL BOTON
    public partial class Form5 : Form
    {
        private Conexion mConexion; //objeto de clase conexion
        public Form5(string dosis)
        {
            InitializeComponent();
            mConexion = new Conexion();

            txtDosis.Text = dosis;
            txtDosis.Enabled = false;

        }

        int hour, minute, second;
        string alarmHour, alarmMinute;

        private void btnReloj_Click(object sender, EventArgs e)
        {
            alarmHour = comboBox1.Text;
            alarmMinute = comboBox2.Text;
            MessageBox.Show("Ya se establecio la alarma");
            //clic start para establecer alarma
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second = DateTime.Now.Second;
            minute = DateTime.Now.Minute;
            hour = DateTime.Now.Hour;

            label11.Text = hour.ToString();
            label12.Text = minute.ToString();
            label13.Text = second.ToString();
            Ring_Alarm();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Detiene el sonido
            axWindowsMediaPlayer1.Ctlcontrols.stop();




            String nom = txtNombre.Text;
            String dosis = txtDosis.Text;

            SqlConnection connection = mConexion.getConexion();
            connection.Open();

            //hereda de la clase donde se guarda el nombre del correo
            string correo = DatosUsuario.CorreoUsuarioActual;
            // Reutilizar el correo que se guardó al iniciar sesión
            string sqlCorreo = "SELECT CuentaID FROM Cuenta WHERE Correo = '" + correo + "'";
            SqlCommand comandoCorreo = new SqlCommand(sqlCorreo, connection);

            object cuentaIdObj = comandoCorreo.ExecuteScalar();
            int cuentaId = (int)cuentaIdObj; //devuelve el ID como un entero paa evitar errores


            //CREO QUE ME FALTA UN IF ANTES DEL QUERY
            string query = " EXEC SP_RestarPastilla";
          //  string query = " UPDATE Usuarios SET Pastillas = Pastillas - @dosis WHERE Nombre = @nombre AND CuentaID = @cuentaId;";
            SqlCommand comando = new SqlCommand(query, connection);
            comando.ExecuteNonQuery();

            comando.Parameters.AddWithValue("@dosis", dosis);
            comando.Parameters.AddWithValue("@nombre", nom);
            comando.Parameters.AddWithValue("@cuentaId", cuentaId);

        }


        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String nom = txtNombre.Text;
            String dosis = txtDosis.Text;

            SqlConnection connection = mConexion.getConexion();
            connection.Open();

            //hereda de la clase donde se guarda el nombre del correo
            string correo = DatosUsuario.CorreoUsuarioActual;
            // Reutilizar el correo que se guardó al iniciar sesión
            string sqlCorreo = "SELECT CuentaID FROM Cuenta WHERE Correo = '" + correo + "'";
            SqlCommand comandoCorreo = new SqlCommand(sqlCorreo, connection);

            object cuentaIdObj = comandoCorreo.ExecuteScalar();
            int cuentaId = (int)cuentaIdObj; //devuelve el ID como un entero paa evitar errores


            string sql =
           "SELECT  Nombre = @nombre, Dosis=@dosis FROM Usuarios WHERE CuentaID = @cuentaId AND Nombre = @nombre";   //REVISAR LO DEL NUMERO CREO QUE ES NOMBRE

            SqlDataReader reader = null;

            try
            {

                SqlCommand comando = new SqlCommand(sql, connection);

                comando.Parameters.AddWithValue("@nombre", nom);
                comando.Parameters.AddWithValue("@dosis", dosis);

                reader = comando.ExecuteReader();

                if (reader.HasRows)    //si hay filas en la bd entonces el lector lee el seleccionado
                {
                    while (reader.Read())
                    {
                        txtNombre.Text = reader.GetString(1);
                        txtDosis.Text = reader.GetInt32(2).ToString();

                    }
                } else
                {   MessageBox.Show("No se encontro este nombre");    }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                reader.Close();   //cerrar lector
                connection.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { }
        void Ring_Alarm()
        {
            if (alarmHour == label11.Text && alarmMinute == label12.Text && second.ToString() == "0")
            {
                axWindowsMediaPlayer1.URL = "C:\\Users\\eilee\\Downloads\\alarma-morning-mix.mp3";
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Start();
            for (int i = 0; i < 24; i++) 
            {
                comboBox1.Items.Add(i);
            }

            for (int j = 0; j<60; j++)
            {
                comboBox2.Items.Add(j);
            }
        }
    }
}
