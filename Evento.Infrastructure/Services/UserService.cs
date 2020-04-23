using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user != null)
                throw new Exception($"User with email '{email}' already exist.");
            await _userRepository.AddUser(new User(userId, role, name, email, password));
        }
    }
}