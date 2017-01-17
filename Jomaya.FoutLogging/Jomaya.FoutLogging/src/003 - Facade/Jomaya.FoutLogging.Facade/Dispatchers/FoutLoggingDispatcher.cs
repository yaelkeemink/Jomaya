using Minor.WSA.EventBus.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minor.WSA.EventBus.Config;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client.Events;
using Minor.WSA.Eventing.Attributes;
using Jomaya.FoutLogging.Infrastructure.Repositories;
using Jomaya.FoutLogging.Infrastructure.DAL;
using System.Text;
using Minor.WSA.Common;
using Newtonsoft.Json;
using Jomaya.FoutLogging.Entities;
using Jomaya.Common.Events;

namespace Jomaya.FoutLogging.Infrastructure
{
    [Minor.WSA.EventBus.Dispatcher.RoutingKey("jomaya.exception.#")]
    public class FoutLoggingDispatcher : EventDispatcher
    {
        private CustomExceptionRepository _repo;

        public FoutLoggingDispatcher(EventBusConfig config, CustomExceptionRepository repo) : base(config)
        {
            _repo = repo;
        }

         public void OnExceptionThrownEvent(ExceptionThrownEvent exceptionThrownEvent)
        {
            var customException = new CustomException()
            {
                ExceptionType = exceptionThrownEvent.ExceptionType.ToString(),
                Message = exceptionThrownEvent.Message,
                StackTrace = exceptionThrownEvent.StackTrace
            };

            _repo.Insert(customException);
        }
        
    }
}
