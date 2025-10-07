using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Runtime.InteropServices.Marshalling;
using MySql.Data.MySqlClient;

namespace DemoRazorPages.Pages
{
    public class OficinaSucursalModel : PageModel
    {
        
        private readonly string cadenaConexion;
        public DataTable Resultado { get; set; } = new DataTable();
        
        public OficinaSucursalModel(IConfiguration config)
        {
            
            this.cadenaConexion = config.GetConnectionString("MySqlConnection");
        }
        public void OnGet()
        {
            Select();
        }
        void Select()
        {
            string consulta = @"SELECT id, nombre, direccion, fechaRegistro
                                FROM oficina
                                WHERE estado=1
                                ORDER BY nombre";
            //string cadenaConexion = configuracion.GetConnectionString("MySqlConnection");
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                conexion.Open();
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                adaptador.Fill(Resultado);
                /* using call to conexion.Close().It is not necessary because of the using statement
                 conexion.Close(); */
            }

        }
    }
}
