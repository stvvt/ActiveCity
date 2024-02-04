using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ActiveCity.Models;
using Microsoft.EntityFrameworkCore;

namespace ActiveCity.Pages
{
    public class LoginModel(Data.ActiveCityContext context) : PageModel
    {
        private readonly Data.ActiveCityContext _context = context;

        [BindProperty]
        public required InputModel Input { get; set; }

        public string? ReturnUrl { get; private set; } = null;

        [TempData]
        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; } = "";

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = "";
        }

        public async Task OnGetAsync(string? returnUrl = "/")
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await AuthenticateUser(Input.Username, Input.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Username ?? ""),
                    new(ClaimTypes.Role, Enum.GetName(user.Role) ?? ""),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                if (ReturnUrl != null) {
                    return LocalRedirect(ReturnUrl);
                }

                return RedirectToPage("/Index", new { area = Enum.GetName(user.Role) });
            }

            // Something failed. Redisplay the form.
            return Page();
        }

        private async Task<User?> AuthenticateUser(string username, string password)
        {
           var user = await _context.User.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
           return user;
        }
    }
}
