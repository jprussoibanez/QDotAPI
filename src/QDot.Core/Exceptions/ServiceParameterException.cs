using System;
using System.Collections.Generic;
using System.Text;

namespace QDot.Core.Exceptions
{
    public class ServiceParameterException : Exception
    {
        public ServiceParameterException()
        {
        }

        public ServiceParameterException(string msg)
            : base(msg)
        {
        }

        public ServiceParameterException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }
}
