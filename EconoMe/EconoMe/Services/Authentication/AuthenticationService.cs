using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EconoMe.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> DoLogin(string email, string password)
        {
            await Task.Run(() => Thread.Sleep(2000));
            return true;
        }
    }
}
