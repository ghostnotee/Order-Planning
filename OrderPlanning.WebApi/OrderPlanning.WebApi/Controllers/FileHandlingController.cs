using System.Data;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderPlanning.WebApi.Data;
using OrderPlanning.WebApi.Models;

namespace OrderPlanning.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileHandlingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FileHandlingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            var dataTable = ReadExcelFile(file);
            var jsonData = JsonConvert.SerializeObject(dataTable);
            var orders = JsonConvert.DeserializeObject<List<Order>>(jsonData);

            foreach (var order in orders)
            {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();

            return Ok(orders);
        }

        [HttpGet]
        public IActionResult EditOrderList()
        {
            List<Order> fixedList = new();
            var orders = _context.Orders.AsQueryable();
            foreach (var order in orders)
            {
                var result = fixedList.LastOrDefault(fo => fo.ID == order.ID);
                if (result is null || result.Amount >= result.MOQ)
                    fixedList.Add(order);
                else
                {
                    result.Amount += order.Amount;
                }
            }

            using var ms = new MemoryStream();
            var workbook = new XLWorkbook();
            var dataSet = new DataSet();

            return Ok(fixedList);
        }


        private static DataTable ReadExcelFile(IFormFile file)
        {
            var dataTable = new DataTable();
            using var ms = new MemoryStream();

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
                    var dataRow = dataTable.NewRow();
                    for (int j = 1; j <= columnSum; j++)
                    {
                        dataRow[j - 1] = worksheet.Cell(i, j).Value;
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }

            return (dataTable);
        }
        
        
    }
}