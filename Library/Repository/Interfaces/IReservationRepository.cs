using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IReservationRepository
    {
        bool AnyReservation(int bookID);
        Task<List<Reservation>> ListAllReservationAsync(int bookID);
        Task SetReservation(Reservation reservation);
    }
}