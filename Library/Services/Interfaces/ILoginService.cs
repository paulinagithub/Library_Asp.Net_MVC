using Library.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ILoginService
    {
        ClaimsIdentity CreateClaimsIdentity(int userID);
        int GetUserID(string login, string password);
    }
}