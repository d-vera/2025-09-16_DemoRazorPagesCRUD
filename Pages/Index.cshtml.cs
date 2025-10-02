using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        public string Mensaje { get; set; }
        public int Numero { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Mensaje = "Hola Mundo Razor Page";
            Numero = 10;
        }
    }
}
