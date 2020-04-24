using System;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands.User;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Json(await _userService.GetAccountAsync(UserId));
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetTickets()
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), register.Email, register.Name,
                register.Password, register.Role);
            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            return Json(await _userService.LoginAsync(login.Email, login.Password));
        }
    }
}