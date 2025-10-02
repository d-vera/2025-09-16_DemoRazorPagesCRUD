using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;
using static Org.BouncyCastle.Math.EC.ECCurve;
namespace DemoRazorPages.Pages
{
    public class OficinaSucursalEditModel : PageModel
    {
        private readonly IConfiguration configuracion;
        [BindProperty]
        public byte Id { get; set; }
        [BindProperty]
        public string Nombre { get; set; }
        [BindProperty]
        public string Direccion { get; set; }
        public OficinaSucursalEditModel(IConfiguration config)
        {
            this.configuracion = config;
        }
        public void OnGet(int id)
        {
            string cadenaConexion = configuracion.GetConnectionString("MySqlConnection");
            string query = @"SELECT id, nombre, direccion
                                FROM oficina
                                WHERE id=@id
                                ORDER BY 2";
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    this.Id = byte.Parse(tabla.Rows[0][0].ToString());
                    this.Nombre = tabla.Rows[0][1].ToString();
                    this.Direccion = tabla.Rows[0][2].ToString();                   
                }
            }
        }
        public IActionResult OnPost()
        {
            string cadenaConexion = configuracion.GetConnectionString("MySqlConnection");

            string query = @"UPDATE oficina SET nombre = @nombre,direccion = @direccion, fechaRegistro=current_timestamp 
                            WHERE id = @id;";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombre", Nombre);
                comando.Parameters.AddWithValue("@direccion", Direccion);
                comando.Parameters.AddWithValue("@id", Id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }

            return RedirectToPage("OficinaSucursal");
        }
    }
}
