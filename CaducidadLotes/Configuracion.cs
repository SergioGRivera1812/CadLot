using CaducidadLotes.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaducidadLotes
{
    public partial class Configuracion : Form
    {
        public Configuracion()
        {
            InitializeComponent();
        }

        public void Configuracion_Load(object sender, EventArgs e)
        {
            CargarConfiguracionCOM();
        }

        private void CargarConfiguracionCOM()
        {
            txtServer.Text = Settings.Default.Server;
            txtDataBase.Text = Settings.Default.DataBase;
            txtUser.Text = Settings.Default.User;
            txtPassword.Text = Settings.Default.Password;
            txtPort.Text = Settings.Default.Port;
        }

        private void GuardarCOM()
        {
            Settings.Default.Server = txtServer.Text;
            Settings.Default.DataBase = txtDataBase.Text;
            Settings.Default.User = txtUser.Text;
            Settings.Default.Password = txtPassword.Text;
            Settings.Default.Port = txtPort.Text;

            Settings.Default.Save();
        }

        private void Configuracion_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Server = txtServer.Text;
            Settings.Default.DataBase = txtDataBase.Text;
            Settings.Default.User = txtUser.Text;
            Settings.Default.Password = txtPassword.Text;
            Settings.Default.Port = txtPort.Text;          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GuardarCOM();
                MessageBox.Show("Configuración guardada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la configuración "+ex, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

