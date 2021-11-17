using System.Collections.Generic;
using Tsak.WebshopProducts2021.Security.Model;

namespace Tsak.WebshopProducts2021.Security
{
    public interface IAuthService
    {
        public string GenerateJwtToken(LoginUser userUserName);
        string Hash(string password);
        List<Permission> GetPermissions(int userId);
    }
    
}