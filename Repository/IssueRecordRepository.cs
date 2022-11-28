using AutoMapper;
using LibraryAppRestapi.Data;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppRestapi.Repository
{
    public class IssueRecordRepository : Repository<IssueRecord>,IIssueRecordRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public IssueRecordRepository(ApplicationDbContext context) : base(context)
        {

        }

        public ICollection<IssueRecord> GetIssueRecordbyStudent(int studentId)
        {
            return ApplicationDbContext.IssueRecords.Where(p => p.StudentId == studentId).ToList();
        }

        public ICollection<IssueRecord> GetIssueRecordByBook(int bookId)
        {
            return ApplicationDbContext.IssueRecords.Where(p => p.BookId == bookId).ToList();
        }

        /*private readonly ApplicationDbContext _context;

        public IssueRecordRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public bool CreateIssueRecord(IssueRecord record)
        {

            _context.Add(record);
            return Save();
        }
        
        public bool UpdateIssueRecord(IssueRecord record)
        {

            _context.Update(record);
            return Save();
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

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool DeleteIssueRecord(IssueRecord record)
        {
            _context.Remove(record);
           return Save();
        }*/
    }
}
