using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataModel;
using Repository;

namespace FileReaderWeb.Pages.V2
{
    public class EditModel : PageModel
    {
        private readonly Repository.ApplicationDBContext _context;

        public EditModel(Repository.ApplicationDBContext context)
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

            var trades =  await _context.Trades.FirstOrDefaultAsync(m => m.TradeId == id);
            if (trades == null)
            {
                return NotFound();
            }
            Trades = trades;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Trades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradesExists(Trades.TradeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TradesExists(int id)
        {
          return (_context.Trades?.Any(e => e.TradeId == id)).GetValueOrDefault();
        }
    }
}
