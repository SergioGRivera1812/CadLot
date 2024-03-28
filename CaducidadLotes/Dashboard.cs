using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CaducidadLotes
{
    public partial class Dashboard : Form
    {
        Conexion cnn;
        Configuracion c = new Configuracion();
        NotifyIcon notifyIcon;
        Timer timer;

        public Dashboard()
        {
            InitializeComponent();

            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;

            timer = new Timer();
            timer.Interval = 60000; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            c.Configuracion_Load(sender, e);
            string s = c.txtServer.Text;
            string b = c.txtDataBase.Text;
            string p = c.txtPort.Text;
            string us = c.txtUser.Text;
            string pass = c.txtPassword.Text;
            cnn = new Conexion(s, b, p, us, pass);
            dataGridLotes.DataSource = load1();
            dataGridLotes.Columns["expDate"].HeaderText = "Fecha de expiración";
            dataGridLotes.Columns["code"].HeaderText = "Codigo";


            dataGridLotes.Columns.Remove("id");
            dataGridLotes.Columns.Remove("active");
            dataGridLotes.Columns.Remove("date");
            dataGridLotes.Columns.Remove("addDate");
            dataGridLotes.Columns.Remove("productId");
            Caducidad();
        }
        private BindingSource bs = new BindingSource();

        private DataTable load1()
        {
            DataTable lots = new DataTable();
            string lotes = "SELECT * FROM nuplen.lots;";
            using (MySqlCommand cmd = new MySqlCommand(lotes, cnn.GetConexion()))
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                lots.Load(reader);
            }
            bs.DataSource = lots;
            return lots;
        }


        public void Caducidad()
        {
            DateTime fechaActual = DateTime.Now;
            TimeSpan lapso = TimeSpan.FromDays(5); 
            bool hayCaducados = false;

            foreach (DataGridViewRow row in dataGridLotes.Rows)
            {
                if (!row.IsNewRow)
                {
                    DateTime fechaTabla = Convert.ToDateTime(row.Cells["expDate"].Value);

                    if (fechaTabla <= fechaActual)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        hayCaducados = true;
                    }
                    else if (fechaTabla > fechaActual && fechaTabla - fechaActual <= lapso)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }

            if (hayCaducados)
            {
                notifyIcon.BalloonTipText = "Hay productos caducados.";
                notifyIcon.ShowBalloonTip(5000);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Caducidad();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracion c = new Configuracion();
            c.Show();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFiltrarMov.Text == "")
            {
                Caducidad();
            }
            else
            {
                FilterData1();
            }
        }

        void FilterData1()
        {
            try
            {
                if (bs.DataSource != null)
                {
                    bs.Filter = $"code LIKE '%{txtFiltrarMov.Text}%' OR code LIKE '%{txtFiltrarMov.Text}%'";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Filtrando datos...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
