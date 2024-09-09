using Sample.Application.interfaces.User;
using Sample.Domain;

namespace Sample.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public User? GetUser(string username, string password)
        {
            return _userRepository.GetUser(username, password);
        }

        public User? GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void RegisterUser(User user)
        {
            var existingUser = _userRepository.GetUser(user.Username, user.Password);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            _userRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = _userRepository.GetUserById(user.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            _userRepository.DeleteUser(id);
        }
    }
}