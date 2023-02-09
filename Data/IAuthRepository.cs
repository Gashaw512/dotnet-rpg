using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string passwor);// register method which retutn int (the id of the user)
        Task<ServiceResponse<string>> Login(string username, string passwor);// login method that retuns string( token string)
        Task<bool> UserExist(string username);// method to check if the user allready exist

    }
}