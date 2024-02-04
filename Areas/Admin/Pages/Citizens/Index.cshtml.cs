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
    public class IndexModel : PageModel
    {
        private readonly ActiveCity.Data.ActiveCityContext _context;

        public IndexModel(ActiveCity.Data.ActiveCityContext context)
        {
            _context = context;
        }

        public IList<Models.Citizen> Citizen { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Citizen = await _context.Citizen.Include(c => c.User).ToListAsync();
        }
    }
}
