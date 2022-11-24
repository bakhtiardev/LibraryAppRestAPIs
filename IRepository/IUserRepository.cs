using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);

      

        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool UserExists(int id);
        bool Save();
    }
}
