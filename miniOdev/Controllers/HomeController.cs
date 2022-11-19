using AutoMapper;
using BL.Models;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using miniOdev.Languages;
using miniOdev.Models;
using System.Diagnostics;

namespace miniOdev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IVeriGirisiServices _veriGirisiServices;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IVeriGirisiServices VeriGirisiServices)
        {
            _logger = logger;
            _mapper = mapper;
            _veriGirisiServices = VeriGirisiServices;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VeriGirisi()
        {         
            return View(new VeriGirisiDTO());
        }

        [HttpPost]
        public JsonResult VeriGirisi(VeriGirisiDTO veriGirisiDTO)
        {
            if (!ModelState.IsValid)
                return Json(veriGirisiDTO);


            DOMAIN.Models.HastaBilgileri hastaBilgileri = _mapper.Map<DOMAIN.Models.HastaBilgileri>(veriGirisiDTO);

            if (_veriGirisiServices.HastaBilgileriKaydet(hastaBilgileri) == 1)
                return Json(new { result = true });
            else
                return Json(new { result = false });
        }

        public IActionResult ListeyiGor()
        {
            List<DOMAIN.Models.HastaBilgileri> hastaBilgileri = _veriGirisiServices.HastaBilgileriGoruntule();

            if(hastaBilgileri == null)
                return View(new List<VeriGirisiDTO>());

            List<Common.ViewModel.ListeyiGorViewModel> listeyiGorViewModels = _mapper.Map<List<Common.ViewModel.ListeyiGorViewModel>>(hastaBilgileri);

            return View(listeyiGorViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}