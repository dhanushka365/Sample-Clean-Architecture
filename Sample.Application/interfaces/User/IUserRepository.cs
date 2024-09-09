namespace Sample.Application.interfaces.User
{
    public interface IUserRepository
    {
        List<Domain.User> GetUsers();
        Domain.User? GetUser(string username, string password);
        Domain.User? GetUserById(int id);
        void AddUser(Domain.User user);
        void UpdateUser(Domain.User user);
        void DeleteUser(int id);
    }
}
