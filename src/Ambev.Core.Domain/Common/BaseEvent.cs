﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Core.Domain.Common
{
    public class BaseEvent<Event>
    {
        public string Message { get; set; }
        public Event Data { get; set; }
        public DateTime CreatedAt { get; set; }

        public BaseEvent(Event data, string message)
        {
            Data = data;
            Message = message;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
