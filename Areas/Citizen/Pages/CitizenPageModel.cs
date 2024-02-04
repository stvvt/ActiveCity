using ActiveCity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActiveCity.Areas.Citizen.Pages;

[Authorize(Roles = "Citizen")]
public abstract class CitizenPageModel: PageModel {}
