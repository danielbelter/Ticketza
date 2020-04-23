using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>();

        public async Task<User> GetUserById(Guid id)
        {
            return await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await Task.FromResult(_users.SingleOrDefault(x =>
                x.Email.ToLowerInvariant() == email.ToLowerInvariant()));
        }

        public async Task AddUser(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateUser(User user)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteUser(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }
    }
}