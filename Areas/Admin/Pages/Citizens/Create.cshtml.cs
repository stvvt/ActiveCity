using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ActiveCity.Data;
using ActiveCity.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ActiveCity.Areas.Admin.Pages.Citizens
{
    public class CreateModel : PageModel
    {
        private readonly ActiveCity.Data.ActiveCityContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ActiveCity.Data.ActiveCityContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            _logger.LogError("xxxxx");
            return Page();
        }

        [BindProperty]
        public Models.Citizen Citizen { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Citizen.User.Role = Role.Citizen;
            ModelState.Clear();
            if (!TryValidateModel(Citizen))
            {
                return Page();
            }


            _context.Citizen.Add(Citizen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
