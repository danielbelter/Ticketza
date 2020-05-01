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
        private readonly ITicketService _ticketService;

        public AccountController(IUserService userService, ITicketService ticketService)
        {
            this._userService = userService;
            this._ticketService = ticketService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Json(await _userService.GetAccountAsync(UserId));
        }

        [HttpGet("tickets")]
        [Authorize]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketService.GetForUserAsync(UserId);
            return Json(tickets);
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