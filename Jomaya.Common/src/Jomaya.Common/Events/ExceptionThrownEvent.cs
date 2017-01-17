using Minor.WSA.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jomaya.Common.Events
{
    public class ExceptionThrownEvent : DomainEvent
    {

        public long ID { get; set; }

        public Type ExceptionType { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }


    }
}
