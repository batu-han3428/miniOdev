using AutoMapper;
using BL.Models;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using miniOdev.Languages;
using miniOdev.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

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

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);

                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public byte[] ExcelOlustur()
        {
            byte[] fileContents;

            List<DOMAIN.Models.HastaBilgileri> hastaBilgileri = _veriGirisiServices.HastaBilgileriGoruntule();
            var table = ToDataTable(hastaBilgileri);


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Hasta_Bilgileri");

                worksheet.Cells["A1"].LoadFromDataTable(table, true, TableStyles.Custom);


                Color beyaz = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                Color acikYesil = System.Drawing.ColorTranslator.FromHtml("#a7f954");
                Color koyuYesil = System.Drawing.ColorTranslator.FromHtml("#57aa05");
                Color siyah = System.Drawing.ColorTranslator.FromHtml("#000000");


                worksheet.Rows[1].Style.Font.Bold = true;
                worksheet.Rows[1].Style.Font.Color.SetColor(OfficeOpenXml.Drawing.eThemeSchemeColor.Text1);
                

                for (var row = 1; row < 2; row++)
                {
                    for (var column = 1; column <= table.Columns.Count; column++)
                    {
                        worksheet.Cells[row, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row, column].Style.Fill.BackgroundColor.SetColor(acikYesil);
                        worksheet.Cells[row, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, column].Style.Indent = 5;
                    }
                }


                for (var row = 2; row <= table.Rows.Count+1; row++)
                {
                    for (var column = 1; column <= table.Columns.Count; column++)
                    {
                        worksheet.Cells[row, column].Style.Font.Bold = false;
                        worksheet.Cells[row, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row, column].Style.Font.Color.SetColor(row % 2 == 0
                           ? siyah
                           : Color.White);
                        worksheet.Cells[row, column].Style.Fill.BackgroundColor.SetColor(row % 2 == 0
                           ? Color.White
                           : koyuYesil);
                        worksheet.Cells[row, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row, column].Style.Indent = 5;
                    }
                }


                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                return fileContents = package.GetAsByteArray();
            }

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