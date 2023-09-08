using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataModel;
using Repository;

namespace FileReaderWeb.Pages.V2
{
    public class DeleteModel : PageModel
    {
        private readonly Repository.ApplicationDBContext _context;

        public DeleteModel(Repository.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DataModel.Trades Trades { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trades == null)
            {
                return NotFound();
            }

            var trades = await _context.Trades.FirstOrDefaultAsync(m => m.TradeId == id);

            if (trades == null)
            {
                return NotFound();
            }
            else 
            {
                Trades = trades;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Trades == null)
            {
                return NotFound();
            }
            var trades = await _context.Trades.FindAsync(id);

            if (trades != null)
            {
                Trades = trades;
                _context.Trades.Remove(Trades);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
