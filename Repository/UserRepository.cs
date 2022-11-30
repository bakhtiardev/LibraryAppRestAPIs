using AutoMapper;
using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public bool userExists(string username)
        {
            return ApplicationDbContext.Users.Any(c => c.Username.Equals(username));
        }

        public User Get(string username)
        {
            return ApplicationDbContext.Users.Where(c => c.Username.Equals(username)).FirstOrDefault();
        }

        /*  private readonly ApplicationDbContext _context;

          public UserRepository(ApplicationDbContext context)
          {
              _context = context;

          }

          public bool CreateUser(User user)
          {
              _context.Add(user);
              return Save();
          }

          public bool DeleteUser(User user)
          {
              _context.Remove(user);
              return Save();
          }

          public User GetUser(int id)
          {
              var user = _context.Users.FirstOrDefault(x => x.Id == id);
              return user;
          }

          public ICollection<User> GetUsers()
          {
             return _context.Users.ToList();
          }

          public bool Save()
          {
              var save = _context.SaveChanges();
              return save > 0 ? true: false;
          }

          public bool UpdateUser(User user)
          {
              _context.Update(user);
              return Save();
          }

          public bool UserExists(int id)
          {
              return _context.Users.Any(p => p.Id == id);
          }*/



    }
}
