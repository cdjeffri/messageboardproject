using MessageBoard.Data;
using MessageBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Services.Users
{
    public class UserService : IUserService
    {
        private MessageBoardDbContext context;
        private IEncryptor encryptor;

        public UserService(IEncryptor encryptor)
        {
            this.encryptor = encryptor;
            this.context = new MessageBoardDbContext();
        }

        public bool Authenticate(string username, string password)
        {
            string encryptedPassword = this.encryptor.Encrypt(password);
            User user = this.context.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == encryptedPassword).SingleOrDefault();

            if (user == null)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        public void Register(User user)
        {
            user.Privileges = "user";
            user.Password = this.encryptor.Encrypt(user.Password);
            this.context.Users.Add(user);
            this.context.SaveChanges();
        }

        public bool Exists(string username)
        {
            User user = this.context.Users.Where(x => x.Username.ToLower() == username.ToLower()).SingleOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InputProvided(string input)
        {
            if (input == null)
            {
                return false;
            }
            else
            {
                return true; 
            }
        }
    }
}