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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user,pass;
            user = txtUsuario.Text;
            pass = txtPass.Text;

            if(user == "admin" && pass == "admin")
            {
                Dashboard d = new Dashboard();
                d.Show();
                this.Hide();
            }
            else
            {
               MessageBox.Show("Credenciales incorrectas","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Configuracion c = new Configuracion();
            c.Show();
        }
    }
}
