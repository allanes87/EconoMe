using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Exceptions
{
    public class RequestProviderException : Exception
    {
        public string Content { get; }

        public RequestProviderException()
        {
        }

        public RequestProviderException(string content)
        {
            Content = content;
        }
    }
}
