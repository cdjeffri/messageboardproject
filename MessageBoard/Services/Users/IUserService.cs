using MessageBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Services.Users
{
    public interface IUserService
    {
        bool Authenticate(string username, string password);
        void Register(User user);
        bool Exists(string username);
        bool InputProvided(string input); 
    }
}
