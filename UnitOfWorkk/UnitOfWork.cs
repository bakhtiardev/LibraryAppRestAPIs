using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Repository;

namespace LibraryAppRestapi.UnitOfWorkk
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; private set; }
     /*   public IMarkRepository Marks { get; private set; }
        public ISubjectRepository Subjects { get; private set; }
        public IGroupRepository Groups { get; private set; }
        public ITeacherRepository Teachers { get; private set; }*/
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
           /* Students = new StudentRepository(_context);
            Marks = new MarkRepository(_context);
            Subjects = new SubjectRepository(_context);
            Teachers = new TeacherRepository(_context);*/
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
