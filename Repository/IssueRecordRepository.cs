using AutoMapper;
using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.Repository
{
    public class IssueRecordRepository : IIssueRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public IssueRecordRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IssueRecord GetIssueRecord(int id)
        {
            return _context.IssueRecords.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<IssueRecord> GetIssueRecordByBook(int bookId)
        {
            return _context.IssueRecords.Where(p => p.BookId == bookId).ToList();
        }

        public ICollection<IssueRecord> GetIssueRecordbyStudent(int studentId)
        {
            return _context.IssueRecords.Where(p=>p.StudentId==studentId).ToList();
        }

        public ICollection<IssueRecord> GetIssueRecords()
        {
            return _context.IssueRecords.ToList();
        }

        public bool IssueRecordExists(int id)
        {
            return _context.IssueRecords.Any(p => p.Id == id);
        }
    }
}
