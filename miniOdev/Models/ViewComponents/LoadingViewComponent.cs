using Microsoft.AspNetCore.Mvc;

namespace miniOdev.Models.ViewComponents
{
    public class LoadingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
