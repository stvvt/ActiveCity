using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ActiveCity.Data;
using ActiveCity.Models;

namespace ActiveCity.Areas.Admin.Pages.Users
{
    public class DetailsModel : AdminPageModel
    {
        private readonly ActiveCity.Data.ActiveCityContext _context;

        public DetailsModel(ActiveCity.Data.ActiveCityContext context)
        {
            _context = context;
        }

        public User AppUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                AppUser = user;
            }
            return Page();
        }
    }
}
