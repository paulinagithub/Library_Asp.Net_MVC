using Library.Models;
using Library.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IReservationService
    {
        bool AnyReservation(int bookID);
        Task<List<ReservationViewModel>> GetReservationDetailAsync(int bookID);
        Task SetReservationAsync(int bookID, int userID);
    }
}