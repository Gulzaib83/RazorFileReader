using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataModel;
using Repository;

namespace FileReaderWeb.Pages.V2
{
    public class CreateModel : PageModel
    {
        private readonly Repository.ApplicationDBContext _context;

        public CreateModel(Repository.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataModel.Trades Trades { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Trades == null || Trades == null)
            {
                return Page();
            }

            _context.Trades.Add(Trades);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
