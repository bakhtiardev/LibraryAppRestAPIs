using LibraryAppRestapi.IRepository;

namespace LibraryAppRestapi.UnitOfWorkk
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IPublisherRepository Publishers { get; }
        IIssueRecordRepository IssueRecords { get; }
        IStudentRepository Students { get; }

        /*ITeacherRepository Teachers { get; }
        ISubjectRepository Subjects { get; }
        IGroupRepository Groups { get; }
        IMarkRepository Marks { get; }
*/
        bool Complete();
    }
}
