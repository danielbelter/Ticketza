using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;

namespace Evento.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._jwtHandler = jwtHandler;
            this._mapper = mapper;
        }

        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user != null)
                throw new Exception($"User with email '{email}' already exist.");
            await _userRepository.AddUser(new User(userId, role, name, email, password));
        }

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new Exception($"Invalid credentials");
            if (user.Password != password)
                throw new Exception($"Invalid credentials");

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            return new TokenDto()
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }

        public async Task<AccountDto> GetAccountAsync(Guid id)
        {
            var user = await _userRepository.GetOrFailAsync(id);
            return _mapper.Map<AccountDto>(user);
        }
    }
}