namespace LibraryAppRestapi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EnrollNum { get; set; }
        public ICollection<IssueRecord> IssueRecords { get; set; }
    }
}
