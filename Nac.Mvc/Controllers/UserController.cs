using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Nac.Dal.Repos.Interfaces;
using System.Security.Claims;

namespace Nac.Mvc.Controllers;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    private readonly IUserRepo _repo;
    private readonly IAppLogging<UserController> _logger;

    public UserController(IUserRepo repo, IAppLogging<UserController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    private async ValueTask<bool> ValidateLoginAsync(string userName, string password)
    {
        var userNameDb = await _repo
            .GetAll()
            .Where(u => u.Name == userName)
            .Select(u => u.Name)
            .FirstOrDefaultAsync();
        if (userNameDb == userName)
        {
            return true;
        }
        return false;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginAsync(string? userName, string? password, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (userName == null /*|| password == null*/)
        {
            return RedirectToAction(nameof(AccessDenied));
        }
        // Normally Identity handles sign in, but you can do it directly
        if (!await ValidateLoginAsync(userName!, password!))
        {
            return RedirectToAction(nameof(AccessDenied));
        }
        var claims = new List<Claim>
                {
                    new Claim("user", userName),
                    new Claim("role", "Member")
                };

        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

        _logger.LogAppInformation($"User {userName} has been logged in");

        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index).RemoveAsyncPostfix(), nameof(HomeController).RemoveController());
        }
    }

    [AllowAnonymous]
    public IActionResult AccessDenied(string? returnUrl = null)
    {
        return View();
    }

    public async Task<IActionResult> LogoutAsync()
    {
        var userName = User.Identity?.Name;
        await HttpContext.SignOutAsync();
        _logger.LogAppInformation($"User {userName} has been logged in");
        return RedirectToAction(nameof(HomeController.Index).RemoveAsyncPostfix(), nameof(HomeController).RemoveController());
    }
}
