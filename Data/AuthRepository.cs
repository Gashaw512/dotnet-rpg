using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        public Task<ServiceResponse<string>> Login(string username, string passwor)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<int>> Register(User user, string passwor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExist(string username)
        {
            throw new NotImplementedException();
        }
    }
}