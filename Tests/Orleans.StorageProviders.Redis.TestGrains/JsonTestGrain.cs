﻿using Orleans.Providers;
using Orleans.Runtime;
using Orleans.StorageProviders.Redis.TestGrainInterfaces;
using System;
using System.Threading.Tasks;

namespace Orleans.StorageProviders.Redis.TestGrains
{
    [StorageProvider(ProviderName = "REDIS-JSON")]
    public class JsonTestGrain : Grain<JsonTestGrainState>, IJsonTestGrain
    {
        public Task Set(string stringValue, int intValue, DateTime dateTimeValue, Guid guidValue, IJsonTestGrain grainValue)
        {
            State.StringValue = stringValue;
            State.IntValue = intValue;
            State.DateTimeValue = dateTimeValue;
            State.GuidValue = guidValue;
            State.GrainValue = grainValue;
            return WriteStateAsync();
        }

        public async Task<Tuple<string, int, DateTime, Guid, IJsonTestGrain>> Get()
        {
            await ReadStateAsync();
            return new Tuple<string, int, DateTime, Guid, IJsonTestGrain>(
              State.StringValue,
              State.IntValue,
              State.DateTimeValue,
              State.GuidValue,
              State.GrainValue);
        }

        public Task Clear()
        {
            return ClearStateAsync();
        }

        public Task<GrainReference> GetReference()
        {
            return Task.FromResult(GrainReference);
        }
    }
}
