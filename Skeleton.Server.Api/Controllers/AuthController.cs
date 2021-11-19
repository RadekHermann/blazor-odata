using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Skeleton.Shared.Domain.IdentityEntity;
using Skeleton.Shared.Domain.Models;

namespace Skeleton.Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<Uzivatel> signInManager;
    private readonly UserManager<Uzivatel> userManager;

    public AuthController(SignInManager<Uzivatel> signInManager, UserManager<Uzivatel> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // await userManager.CreateAsync(new Uzivatel
        // {
        //     UserName = "radek.hermann",
        //     Email = "radek.hermann@gmail.com",
        //     Jmeno = "Radek",
        //     Prijmeni = "Hermann",
        // }, "Radek123");

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var result = signInManager.PasswordSignInAsync(model.Username, model.Password, false, false).Result;
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpGet]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("currentUser")]
    public async Task<IActionResult> CurrentUser()
    {
        var user = await userManager.GetUserAsync(User);
        return Ok(user);
    }
}