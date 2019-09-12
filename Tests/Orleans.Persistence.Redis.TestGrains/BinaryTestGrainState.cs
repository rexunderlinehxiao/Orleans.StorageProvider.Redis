﻿using Orleans.Persistence.Redis.TestGrainInterfaces;
using System;

namespace Orleans.Persistence.Redis.TestGrains
{
    public class BinaryTestGrainState
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public Guid GuidValue { get; set; }
        public IBinaryTestGrain GrainValue { get; set; }
    }
}
