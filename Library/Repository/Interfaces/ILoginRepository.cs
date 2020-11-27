using System.Threading.Tasks;

namespace Library.Repository
{
    public interface ILoginRepository
    {
        int GetUserID(string login, byte[] password);
    }
}