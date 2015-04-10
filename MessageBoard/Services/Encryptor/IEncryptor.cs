using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Services
{
    public interface IEncryptor
    {
        string Encrypt(string input);
    }
}
