using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetErandrosTools.Exceptions
{
    public class UnauthorizedRequestException : Exception
    {
        private Uri requestUri;

        public UnauthorizedRequestException(Uri requestUri)
        {
            this.requestUri = requestUri;
        }

        public override string Message
        {
            get
            {
                return $"(Unauthorized API request from Viper Client) to: {requestUri}";
            }
        }
    }
}
