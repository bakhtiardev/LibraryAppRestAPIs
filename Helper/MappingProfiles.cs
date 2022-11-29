using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.IRepository;
using LibraryAppRestapi.Models;
using LibraryAppRestapi.UnitOfWorkk;

namespace LibraryAppRestapi.Helper
{                                       
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>();
            CreateMap<Author,AuthorDto>();
            CreateMap<Student,StudentDto>();
            CreateMap<Publisher,PublisherDto>();
            CreateMap<IssueRecord, IssueRecordDto>();
            CreateMap<User, UserDto>();
            CreateMap<IUnitOfWork, UnitOfWork>();

            CreateMap<BookDto, Book>();
            CreateMap<AuthorDto, Author>();
            CreateMap<StudentDto, Student>();
            CreateMap<PublisherDto, Publisher>();
            CreateMap<IssueRecordDto, IssueRecord>();
            CreateMap<UserDto, User>();
            CreateMap<UnitOfWork, IUnitOfWork>();
        }
    }
}
