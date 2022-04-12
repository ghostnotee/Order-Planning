using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OrderPlanning.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileHandlingController : ControllerBase
    {
        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            var dataTable = new DataTable();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);

                using (var workbook = new XLWorkbook(ms))
                {
                    var worksheet = workbook.Worksheet(1);
                    var columnSum = worksheet.LastColumnUsed().ColumnNumber();
                    var rowSum = worksheet.LastRowUsed().RowNumber();

                    for (int i = 1; i <= columnSum; i++)
                    {
                        dataTable.Columns.Add(worksheet.Cell(1, i).Value.ToString());
                    }

                    for (int i = 2; i <= rowSum; i++)
                    {
                        var dr = dataTable.NewRow();
                        for (int j = 1; j <= columnSum; j++)
                        {
                            dr[j-1] = worksheet.Cell(i, j).Value;
                        }

                        dataTable.Rows.Add(dr);
                    }
                }
            }

            var jsonResult = JsonConvert.SerializeObject(dataTable);
            return Ok(jsonResult);
        }
    }
}