using AutoMapper;
using BL.Models;
using Common.DTO;
using DOMAIN.Context;
using DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using miniOdev.Helpers;
using miniOdev.Languages;
using miniOdev.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using Quartz;
using Quartz.Impl;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Claims;

namespace miniOdev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IVeriGirisiServices _veriGirisiServices;
        private readonly IJobServices _jobServices;
        private readonly UserManager<CustomUser> userManager;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, IVeriGirisiServices VeriGirisiServices, IJobServices JobServices, UserManager<CustomUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _veriGirisiServices = VeriGirisiServices;
            _jobServices = JobServices;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(/*File(
                fileContents: ExcelOlustur(),
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "TextExcel.xlsx"
               */ );
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

        [Authorize]
        public IActionResult JobPage()
        {

            return View(new JobTableDTO());
        }

        [HttpPost]
        [Authorize]
        public JsonResult JobPage(JobTableDTO jobTableDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(jobTableDTO);


                jobTableDTO.CustomUserId = userManager.GetUserId(User);
                jobTableDTO.JOB_KEY = "Job1";
                jobTableDTO.IS_ACTIVE = true;
                jobTableDTO.DESCRIPTION = "JOB1";

                if (jobTableDTO.JobTypeId == 2)
                    jobTableDTO.DAY = jobTableDTO.DAY - 1;


                DOMAIN.Models.JobTable jobTable = _mapper.Map<DOMAIN.Models.JobTable>(jobTableDTO);

                if (_jobServices.JobTableKaydet(jobTable) == 1)
                {
                    //Console.WriteLine(SchedulerHelper.ResetJob());
                    return Json(new { result = true });
                }
                else
                    return Json(new { result = false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { result = false });
            }
            
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