using TestTask.Entities;
using TestTask.Entities.Models;

namespace TestTask.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> Authorize(UserAuthorizeModel user);

        Task<string> Register(UserRegisterModel user);

        IEnumerable<User> GetAllUsers();
    }
}
