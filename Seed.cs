using LibraryAppRestapi.Data;
using LibraryAppRestapi.Models;
using System.Diagnostics.Metrics;

namespace LibraryAppRestapi
{
    public class Seed
    {
        private readonly ApplicationDbContext dataContext;
        public Seed(ApplicationDbContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.BookAuthors.Any())
            {
                var bookAuthors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = new Book()
                        {
                            Title = "Heavens Boy",
                            Isbn = 1111111,
                             Publisher = new Publisher()
                            {
                                PublisherName="Jhageer Series"
                            },
                            IssueRecords = new List<IssueRecord>()
                            {
                                new IssueRecord
                                {
                                    IssueDate = DateTime.Now.Date,
                                    ReturnDate = DateTime.Now.Date,
                                    Student = new Student()
                                    {
                                        Name="Bakhtiar",
                                        EnrollNum=1111,
                                    }
                                },
                                new IssueRecord
                                {
                                    IssueDate = DateTime.Now.Date,
                                    ReturnDate = DateTime.Now.Date,
                                    Student= new Student()
                                    {
                                        Name="Hussain",
                                        EnrollNum=2222,
                                    }
                                }
                            }

                        },
                        Author = new Author()
                        {
                            AuthorName="Wahab Malik",
                            Address="Sector G-9/3"
                        }
                    },
                    new BookAuthor()
                    {
                        Book = new Book()
                        {
                            Title = "Gods Plan",
                            Isbn = 22222,
                            Publisher = new Publisher()
                            {
                                PublisherName="Pengiun Series"
                            },
                            IssueRecords = new List<IssueRecord>()
                            {
                                new IssueRecord
                                {   
                                    IssueDate = new DateTime(2022, 1, 9),
                                    ReturnDate = new DateTime(2022,2,8),
                                    Student = new Student()
                                    {
                                        Name="Ali",
                                        EnrollNum=3333,
                                    }
                                },
                                new IssueRecord
                                {
                                    IssueDate = new DateTime(2021, 2, 1),
                                    ReturnDate = new DateTime(2021,3,5),
                                    Student= new Student()
                                    {
                                        Name="Usama",
                                        EnrollNum=4444,
                                    }
                                }
                            }

                        },
                        Author = new Author()
                        {
                            AuthorName="Sayam Abadat",
                            Address="Sector F-8"
                        }
                    }



                };
                dataContext.BookAuthors.AddRange(bookAuthors);
                dataContext.SaveChanges();
            }
        }
    }
}

