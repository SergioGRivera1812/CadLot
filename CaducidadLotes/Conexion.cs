using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace CaducidadLotes
{
    class Conexion
    {
        private MySqlConnection conexion;
        private string cadenaConexion;

        public Conexion(string server, string database, string port, string user, string password)
        {
            cadenaConexion = "Server=" + server +
                             "; Database=" + database +
                             "; Port=" + port +
                             "; User Id=" + user +
                             "; Password=" + password;
        }

        public string getConexion()
        {
            // Aquí debes retornar la cadena de conexión en lugar de la conexión en sí.
            string connectionString = cadenaConexion;
            return connectionString;
        }

        public MySqlConnection GetConexion()
        {
            if (conexion == null)
            {
                conexion = new MySqlConnection(cadenaConexion);
            }

            // Verificar si la conexión está cerrada y abrirla si es necesario
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }

            return conexion;
        }

        public MySqlConnection close()
        {

            conexion.Close();

            return conexion;
        }
    }
}
