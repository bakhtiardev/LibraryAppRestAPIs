using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Repository;

namespace LibraryAppRestapi.UnitOfWorkk
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; private set; }
        public IAuthorRepository Authors { get; private set; }
        public IBookRepository Books { get; private set; }
        public IIssueRecordRepository IssueRecords { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IPublisherRepository Publishers { get; private set; }

     /*   public IMarkRepository Marks { get; private set; }
        public ISubjectRepository Subjects { get; private set; }
        public IGroupRepository Groups { get; private set; }
        public ITeacherRepository Teachers { get; private set; }*/
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Authors = new AuthorRepository(_context);
            Books=new BookRepository(_context);
            IssueRecords = new IssueRecordRepository(_context);
            Students = new StudentRepository(_context);
            Publishers = new PublisherRepository(_context);

           /* Students = new StudentRepository(_context);
            Marks = new MarkRepository(_context);
            Subjects = new SubjectRepository(_context);
            Teachers = new TeacherRepository(_context);*/
        }


        public bool Complete()
        {
            var save= _context.SaveChanges();
            return save > 0? true: false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
