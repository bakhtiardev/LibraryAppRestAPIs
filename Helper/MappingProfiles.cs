using AutoMapper;
using LibraryAppRestapi.Dto;
using LibraryAppRestapi.Models;

namespace LibraryAppRestapi.Helper
{                                       
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
