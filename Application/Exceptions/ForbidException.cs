﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException()
        {
        }

        public ForbidException(string? message) : base(message)
        {
        }

        public ForbidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ForbidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
