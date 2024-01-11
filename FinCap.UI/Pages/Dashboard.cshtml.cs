using FinCap.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinCap.UI.Pages
{
    public class DashboardModel : FinCapPageModel
    {
        protected override bool PrecisaLogar() => true;

        private readonly ServiceFactory _services;

        public DashboardModel(ServiceFactory services) : base(services) { _services = services; }

        public void OnGet()
        {
        }
    }
}
