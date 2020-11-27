using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Library.Repository
{

    public class LoginRepository : ILoginRepository
    {      
        private readonly LibraryDBContext _dbContext;

        public LoginRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetUserID(string login, byte[] password)
        {
            return _dbContext.Users.Where(w => w.UserName == login && w.UserPassword == password).Select(s => s.Id).FirstOrDefault();
        }
    }
}
