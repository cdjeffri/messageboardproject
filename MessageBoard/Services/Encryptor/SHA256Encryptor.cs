using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MessageBoard.Services
{
    public class SHA256Encryptor : IEncryptor
    {
        public string Encrypt(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            SHA256 value = SHA256.Create();
            byte[] hashedBytes = value.ComputeHash(bytes);
            return Convert.ToBase64String(hashedBytes);
        }
    }
}