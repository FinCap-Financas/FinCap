using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Web;

namespace FinCap.UI.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : FinCapPageModel
    {
        protected override bool PrecisaLogar() => false;

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            try
            {
                var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                var erro = exceptionHandlerPathFeature.Error;

                if (erro.GetType() == typeof(UsuarioNaoLogadoException))
                {
                    var returnTo = HttpUtility.UrlDecode(erro.Message);

                    SaveValue("returnTo", returnTo, null);

                    return Redirect("/?rto=logout");
                }
            }
            catch (Exception ex)
            { }

            return Page();
        }
    }

    [Serializable]
    public class UsuarioNaoLogadoException : Exception
    {
        public UsuarioNaoLogadoException(string path) : base(path) { }
    }
}
