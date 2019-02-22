﻿using System.Collections.Generic;

namespace MainMicroService.Models
{
    public class UserConnection
    {
        public int Id { get; set; }

        public IList<string> SignalrConnections { get; set; }

        public IList<string> Devices { get; set; }
    }
}