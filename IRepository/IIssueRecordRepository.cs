using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IIssueRecordRepository
    {
        ICollection<IssueRecord> GetIssueRecords();
        IssueRecord GetIssueRecord(int id);
        ICollection<IssueRecord> GetIssueRecordbyStudent(int studentId);
        ICollection<IssueRecord> GetIssueRecordByBook(int bookId);
        bool IssueRecordExists(int id);
    }
}
