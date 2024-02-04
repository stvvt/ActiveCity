using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ActiveCity.Data;
using ActiveCity.Models;

namespace ActiveCity.Areas.Admin.Pages.Citizens
{
    public class DeleteModel : PageModel
    {
        private readonly ActiveCity.Data.ActiveCityContext _context;

        public DeleteModel(ActiveCity.Data.ActiveCityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Citizen Citizen { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen.FirstOrDefaultAsync(m => m.Id == id);

            if (citizen == null)
            {
                return NotFound();
            }
            else
            {
                Citizen = citizen;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen != null)
            {
                Citizen = citizen;
                _context.Citizen.Remove(Citizen);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
