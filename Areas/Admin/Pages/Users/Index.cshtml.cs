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
    public class IndexModel : AdminPageModel
    {
        private readonly ActiveCity.Data.ActiveCityContext _context;

        public IndexModel(ActiveCity.Data.ActiveCityContext context)
        {
            _context = context;
        }

        public IList<User> AppUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            AppUser = await _context.User.ToListAsync();
        }
    }
}
