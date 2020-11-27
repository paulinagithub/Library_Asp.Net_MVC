using AutoMapper;
using Library.Models;
using Library.Repository;
using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ReservationViewModel>> GetReservationDetail(int bookID)
        {
            var reservationList =  await _reservationRepository.ListAllReservationAsync(bookID);
            return _mapper.Map<List<ReservationViewModel>>(reservationList);

        }
        public async Task SetReservation(int bookID, int userID)
        {
            await _reservationRepository.SetReservation(CreateReservationObject(bookID, userID));           
        }
        public bool AnyReservation(int bookID)
        {
            return _reservationRepository.AnyReservation(bookID);
        }
        public Reservation CreateReservationObject(int bookID, int userID)
        {
            Reservation reservation = new Reservation();
            reservation.ReservationDate = DateTime.Now;
            reservation.UserId = userID;
            reservation.BookId = bookID;

            return reservation;
        }
    }
}
