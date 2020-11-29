using AutoMapper;
using Library.Repository;
using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
        }
        public int GetUserID(string login, string password)
        {
            var hashPassword = ComputeSha256Hash(password);
            return _loginRepository.GetUserID(login, hashPassword);
        }
        public ClaimsIdentity CreateClaimsIdentity(int userID)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, userID.ToString())
            };
            
            return new ClaimsIdentity(claims, "Login");
        }
        private byte[] ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            }
        }
    }
}
