using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Concrete
{
    public class ExcelManager
    {
        static DataTable ToDataTable<T>(List<T> items)
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
        public static byte[] ExcelOlustur(List<DOMAIN.Models.HastaBilgileri> hastaBilgileri)
        {
            byte[] fileContents;

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


                for (var row = 2; row <= table.Rows.Count + 1; row++)
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
    }
}
