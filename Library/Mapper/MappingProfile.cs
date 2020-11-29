using AutoMapper;
using Library.Models;
using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Book Mapper
            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();
            //Reservation Mapper
            CreateMap<Reservation, ReservationViewModel>();
            CreateMap<ReservationViewModel, Reservation>();
        }
    }
}
