using LibraryAppRestapi.IRepository;

namespace LibraryAppRestapi.UnitOfWorkk
{
    internal interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        /*ITeacherRepository Teachers { get; }
        ISubjectRepository Subjects { get; }
        IGroupRepository Groups { get; }
        IMarkRepository Marks { get; }
*/
        int Complete();
    }
}
