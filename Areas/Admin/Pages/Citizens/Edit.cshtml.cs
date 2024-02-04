using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ActiveCity.Data;
using ActiveCity.Models;

namespace ActiveCity.Areas.Admin.Pages.Citizens
{
    public class EditModel : PageModel
    {
        private readonly ActiveCity.Data.ActiveCityContext _context;

        public EditModel(ActiveCity.Data.ActiveCityContext context)
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

            var citizen =  await _context.Citizen
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citizen == null)
            {
                return NotFound();
            }
            Citizen = citizen;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var existing = await _context.Citizen.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == Citizen.Id);

            if (existing == null) {
                return NotFound();
            }

            Citizen.User.Role = Role.Citizen;
            Citizen.User.Password = existing.User.Password;
            ModelState.Clear();
            if (!TryValidateModel(Citizen))
            {
                return Page();
            }

            _context.Entry(existing).CurrentValues.SetValues(Citizen);
            _context.Entry(existing).State = EntityState.Modified;
            _context.Entry(existing.User).CurrentValues.SetValues(Citizen.User);
            _context.Entry(existing.User).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private bool CitizenExists(int id)
        {
            return _context.Citizen.Any(e => e.Id == id);
        }
    }
}
