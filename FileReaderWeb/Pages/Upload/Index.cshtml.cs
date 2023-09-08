using DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Repository;


namespace FileReaderWeb.Pages.Trades
{
    public class IndexModel : PageModel
    {
        private readonly Repository.ApplicationDBContext _context;

        public IEnumerable<DataModel.Trades> trades { get; set; }  

        public IndexModel(Repository.ApplicationDBContext context)
        {
            this._context = context;
        }

        [BindProperty]
        public IFormFile File { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (File != null && File.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    
                    await File.CopyToAsync(stream);
                    stream.Position = 0;
                    var workbook = new XSSFWorkbook(stream); // For .xlsx files

                    ISheet sheet = workbook.GetSheetAt(0);
                    int rowCount = sheet.PhysicalNumberOfRows;

                    for (int row = 1; row < rowCount; row++) 
                    {
                        IRow dataRow = sheet.GetRow(row);

                        var data = new DataModel.Trades()
                        {
                            StockSymbol = dataRow.GetCell(1).ToString(), 
                            TradeTime = Convert.ToDateTime( dataRow.GetCell(2).ToString()),
                            TradePrice = Convert.ToDouble( dataRow.GetCell(3).ToString()),
                            Qunatity = Convert.ToInt32( dataRow.GetCell(4).ToString()),
                            BuyerId = Convert.ToInt32(dataRow.GetCell(5).ToString()),
                            SellerId = Convert.ToInt32(dataRow.GetCell(6).ToString()),
                            TradeType = dataRow.GetCell(7).ToString(),
                            CommissionFee = Convert.ToDouble(dataRow.GetCell(8).ToString())

                        };

                        _context.Trades.Add(data);
                    }

                    await _context.SaveChangesAsync();

                }
            }

            return RedirectToPage("/V2/index");
        }
    }
}
