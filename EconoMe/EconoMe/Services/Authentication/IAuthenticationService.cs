using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EconoMe.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> DoLogin(string email, string password);
    }
}
