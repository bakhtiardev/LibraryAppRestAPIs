namespace LibraryAppRestapi.Dto
{
    public class IssueRecordDto
    {
        public int Id { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int StudentId { get; set; }
        public int BookId { get; set; }
    }
}
