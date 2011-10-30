﻿using System;
using NServiceBus;

namespace NES.Sample.Messages
{
    public class Command : IMessage
    {
        private Guid _id = GuidComb.NewGuidComb();
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}