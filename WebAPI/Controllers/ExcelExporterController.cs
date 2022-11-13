using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.SGCityServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelExporterController : ControllerBase
    {
        [HttpPost]
        public async Task<FileContentResult> GenerateExcel([FromForm] string id, [FromServices] SGCityServiceSearchService sGCity)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            byte[] bytes;
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("ServicesSheet");
                
                sheet.Cells[1, 1].Value = "DienstStelle";
                sheet.Cells[1, 2].Value = "Thema";
                sheet.Cells[1, 3].Value = "Merkblatt Link";
                sheet.Cells[1, 4].Value = "Direktlink";

                var services = sGCity.GetAll().Where(s => id.Split(", ").Contains(s.direktlink_url));

                int row = 2;
                foreach (var service in services)
                {
                    sheet.Cells[row, 1].Value = service.dienststelle;
                    sheet.Cells[row, 2].Value = service.thema;
                    sheet.Cells[row, 3].Value = service.merkblatt_link ?? string.Empty;
                    sheet.Cells[row, 4].Value = service.direktlink_url ?? string.Empty;

                    row++;
                }

                bytes = await package.GetAsByteArrayAsync();
            }

            var file = new FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            file.FileDownloadName = "St.GallenDienstleistungen.xlsx";

            return file;
        }
    }
}
