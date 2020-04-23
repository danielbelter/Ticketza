using System;
using System.Threading.Tasks;
using Evento.Core.Domain;

namespace Evento.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }
}