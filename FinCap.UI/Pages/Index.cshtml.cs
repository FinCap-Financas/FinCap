using FinCap.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FinCap.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ServiceFactory _services;

        public IndexModel(ServiceFactory services)
        {
            _services = services;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Nome { get; set; }

        [BindProperty]
        public string Senha { get; set; }

        public void OnGet()
        {

        }
        
        public void OnPost()
        {

        }
        
        public void OnPostCadastro()
        {
            var usuario = _services.Usuarios.Cadastro(Nome, Email, Senha);
            Console.WriteLine(JsonConvert.SerializeObject(usuario));
        }
    }
}
