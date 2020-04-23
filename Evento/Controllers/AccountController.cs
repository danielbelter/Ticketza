using System;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands.User;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetTickets()
        {
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register register)
        {
            await userService.RegisterAsync(Guid.NewGuid(), register.Email, register.Name,
                register.Password, register.Role);
            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            throw new NotImplementedException();
        }
    }
}