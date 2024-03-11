using TestTask.DAL.Interfaces;
using TestTask.DAL.Repositories;
using TestTask.Entities;
using TestTask.Entities.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.ConcrateServices
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;


        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Authorize(UserAuthorizeModel user)
        {
            var userToAuthorize = _userRepository.GetAll().Where(u => u.UserName == user.UserName).FirstOrDefault();

            if (userToAuthorize != null && Hasher.ComputeSha256Hash(user.Password) == userToAuthorize.Password) 
            {
                return JwtTokenGenerator.GenerateJwtToken(userToAuthorize.UserName);
            }
            else 
            {
                return null;
            }
        }

        public async Task<string> Register(UserRegisterModel user)
        {
            if (IsUserExsist(user.UserName))
                return null;

            var newUser = new User()
            {
                UserName = user.UserName,

                Password = Hasher.ComputeSha256Hash(user.Password)
            };

            await _userRepository.CreateAsync(newUser);

            return JwtTokenGenerator.GenerateJwtToken(newUser.UserName);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }


        private bool IsUserExsist(string userName)
        {
            var user = _userRepository.GetAll().Where(u => u.UserName == userName).FirstOrDefault();

            return user != null;
        }
    }
}
