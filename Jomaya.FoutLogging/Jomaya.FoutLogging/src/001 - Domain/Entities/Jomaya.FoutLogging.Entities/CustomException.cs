using System;
using System.ComponentModel.DataAnnotations;

namespace Jomaya.FoutLogging.Entities
{
    public class CustomException
    {
        public long Id { get; set; }

        public string ExceptionType { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}
