using FinCap.Application.Services;
using FinCap.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace FinCap.UI.Pages
{
    public abstract class FinCapPageModel : PageModel
    {
        private Dictionary<string, string> _cookies = new Dictionary<string, string>();
        private readonly ServiceFactory _services;

        public string TokenLogin
        {
            get => GetValue("tokenLogin");
            set => SaveValue("tokenLogin", value, null);
        }

        private Usuario _usuarioLogado;
        public Usuario UsuarioLogado
        {
            get
            {
                if (_usuarioLogado is null && !string.IsNullOrEmpty(TokenLogin) && Guid.TryParse(TokenLogin, out Guid uidUsuario))
                    _services.Usuarios.Get(uidUsuario);

                return _usuarioLogado;
            }
        }

        public bool EstaLogado()
        {
            return UsuarioLogado is not null;
        }

        public void DeslogarUsuario()
        {
            _usuarioLogado = null;
            TokenLogin = null;
        }

        protected abstract bool PrecisaLogar();

        public string GetValue(string key)
        {
            if (_cookies.ContainsKey(key))
                return _cookies[key];

            return _cookies[key] = Request.Cookies[key] ?? string.Empty;
        }

        public string GetValueAndErase(string key)
        {
            string value = GetValue(key);

            if (!string.IsNullOrEmpty(value))
            {
                _cookies.Remove(key);
                SaveValue(key, string.Empty, 0);
            }

            return value;
        }

        public void SaveValue(string key, object value, int? diasParaExpirar)
        {
            value = value.ToString();

            CookieOptions cookieOptions = new()
            {
                IsEssential = true,
                HttpOnly = true,
                Secure = true,
            };

            if (diasParaExpirar is not null)
            {
                cookieOptions.Expires = DateTime.Now.AddDays(diasParaExpirar.Value);
            }
            else cookieOptions.Expires = DateTime.Now.AddDays(1000);

            Response.Cookies.Append(key, value.ToString(), cookieOptions);

            if (_cookies.ContainsKey(key))
                _cookies[key] = value.ToString();
            else
                _cookies.Add(key, value.ToString());
        }

        public void AdicionaSucesso(string mensagem) => SaveValue("MsgSucesso", mensagem, null);
        public string GetSucesso() => GetValueAndErase("MsgSucesso");
        
        public void AdicionaAviso(string mensagem) => SaveValue("MsgAviso", mensagem, null);
        public string GetAviso() => GetValueAndErase("MsgAviso");
        
        public void AdicionaErro(string mensagem) => SaveValue("MsgErro", mensagem, null);
        public string GetErro() => GetValueAndErase("MsgErro");

        public string GetPath()
        {
            var path = Request.Path.ToString();

            if (path.StartsWith(@"\") || path.StartsWith(@"/"))
                path.Substring(1);

            return path;
        }

        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            base.OnPageHandlerSelected(context);

            if (!EstaLogado() && PrecisaLogar())
                throw new UsuarioNaoLogadoException(HttpUtility.UrlEncode(GetPath() + Request.QueryString));
        }
    }
}
