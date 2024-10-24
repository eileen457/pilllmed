using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ver
{                                                     //ALARMA FUNCIONA PERO TENGO QUE PONER UN STOP AL SONIDO NO A LOS NUMEROS
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        int hour, minute, second;
        string alarmHour, alarmMinute;

        private void btnReloj_Click(object sender, EventArgs e)
        {
            alarmHour=comboBox1.Text;
            alarmMinute=comboBox2.Text;
            MessageBox.Show("Ya se establecio la alarma");
     //clic start para establecer alarma
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            second=DateTime.Now.Second;
            minute=DateTime.Now.Minute;
            hour=DateTime.Now.Hour;

            label11.Text= hour.ToString();
            label12.Text= minute.ToString();
            label13.Text= second.ToString();
            Ring_Alarm();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Detiene el sonido
            axWindowsMediaPlayer1.Ctlcontrols.stop();
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
