using ActiveCity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActiveCity.Areas.Admin.Pages;

[Authorize(Roles = "Admin")]
public abstract class AdminPageModel: PageModel {}
