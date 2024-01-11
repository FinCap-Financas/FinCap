using FinCap.Application.Services;
using FinCap.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FinCap.UI.Pages
{
    public class IndexModel : FinCapPageModel
    {
        protected override bool PrecisaLogar() => false;

        private readonly ServiceFactory _services;

        public IndexModel(ServiceFactory services) : base(services) { _services = services; }

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

                return Page();
            }
            catch (Exception ex)
            {
                AdicionaErro(ex.Message);
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                var usuario = _services.Usuarios.Login(Email, Senha);

                if (usuario is not null)
                {
                    TokenLogin = usuario.Uid.ToString();
                    UsuarioLogado = usuario;

                    return Redirect("/dashboard");
                }

                return Page();
            }
            catch (Exception ex)
            {
                AdicionaErro(ex.Message);
                return Page();
            }
        }

        public IActionResult OnPostCadastro()
        {
            try
            {
                var usuario = _services.Usuarios.Cadastro(Nome, Email, Senha);
                AdicionaSucesso("Usuário cadastrado com sucesso!");
                return Page();
            }
            catch (Exception ex)
            {
                AdicionaErro(ex.Message);
                return Page();
            }
        }
    }
}
