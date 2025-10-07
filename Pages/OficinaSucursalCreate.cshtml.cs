using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace DemoRazorPages.Pages
{
    public class OficinaSucursalCreateModel : PageModel
    {
        private readonly string cadenaConexion;
        [BindProperty]
        public string Nombre { get; set; }
        [BindProperty]
        public string Direccion { get; set; }
        public OficinaSucursalCreateModel(IConfiguration config)
        {
            this.cadenaConexion = config.GetConnectionString("MySqlConnection");
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            string query= @"INSERT INTO oficina(nombre,direccion)
                            VALUES(@nombre,@direccion)";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            { 
            MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombre", Nombre);
                comando.Parameters.AddWithValue("@direccion", Direccion);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
                return RedirectToPage("OficinaSucursal");
        }
    }
}
