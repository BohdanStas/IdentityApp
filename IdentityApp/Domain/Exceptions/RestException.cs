using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Exceptions
{
    public class RestException : Exception
    {
        public object Errors { get; set; }

        public HttpStatusCode Code { get; set; }

        public RestException(string errors, HttpStatusCode code)
        {
            Errors = errors;
            Code = code;
        }
    }
}
