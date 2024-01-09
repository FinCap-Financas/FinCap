using FinCap.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FinCap.UI.Pages
{
    public class IndexModel : FinCapPageModel
    {
        protected override bool PrecisaLogar() => false;

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

        public IActionResult OnGet()
        {
            try
            {
                if (Request.Query.ContainsKey("rto") && Request.Query["rto"].ToString().ToLower() == "logout")
                {
                    DeslogarUsuario();
                    return Redirect("/");
                }

                if (EstaLogado())
                    return Redirect("/dashboard");
            }
            catch (Exception ex)
            {

            }
        }

        public IActionResult OnPost()
        {
            try
            {
                string usuario = null;
                //var usuario = _services.Usuarios.FazerLogin(Email, Senha);

                if (usuario is not null)
                {
                    TokenLogin = usuario;
                    UsuarioLogado = usuario;

                    return Redirect("/dashboard");
                }

                return Page();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void OnPostCadastro()
        {
            var usuario = _services.Usuarios.Cadastro(Nome, Email, Senha);
            Console.WriteLine(JsonConvert.SerializeObject(usuario));
        }
    }
}
