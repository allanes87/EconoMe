using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Exceptions
{
    public class NotConnectedException : RequestProviderException
    {
        public NotConnectedException(string content = null) : base(content)
        {
        }
    }
}
