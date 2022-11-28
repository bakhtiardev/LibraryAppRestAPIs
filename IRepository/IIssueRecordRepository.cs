using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.IRepository
{
    public interface IIssueRecordRepository : IRepository<IssueRecord>
    {
        ICollection<IssueRecord> GetIssueRecordbyStudent(int studentId);
        ICollection<IssueRecord> GetIssueRecordByBook(int bookId);



        /* ICollection<IssueRecord> GetIssueRecords();
         IssueRecord GetIssueRecord(int id);
         ICollection<IssueRecord> GetIssueRecordbyStudent(int studentId);
         ICollection<IssueRecord> GetIssueRecordByBook(int bookId);
         bool IssueRecordExists(int id);
         bool CreateIssueRecord(IssueRecord record);
         bool UpdateIssueRecord(IssueRecord record);
         bool DeleteIssueRecord(IssueRecord record);
         bool Save();*/
    }
}
