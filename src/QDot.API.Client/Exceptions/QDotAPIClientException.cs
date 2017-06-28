using System;
using System.Collections.Generic;
using System.Text;

namespace QDot.API.Client.Exceptions
{
    [Serializable]
    public class QDotAPIClientException : Exception
    {
        public QDotAPIClientException()
        {
        }

        public QDotAPIClientException(string msg)
            : base(msg)
        {
        }

        public QDotAPIClientException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }
}
