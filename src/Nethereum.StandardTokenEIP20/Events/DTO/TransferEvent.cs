﻿using System;
using System.Numerics;
using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;

namespace Conflux.StandardTokenEIP20.Events.DTO
{
    [Event("Transfer")]
    [Obsolete("Please use TransferEventDTO instead")]
    public class Transfer:IEventDTO
    {
        [Parameter("address", "from", 1, true)]
        public string AddressFrom { get; set; }

        [Parameter("address", "to", 2, true)]
        public string AddressTo { get; set; }

        [Parameter("uint", "value", 3)]
        public BigInteger Value { get; set; }
    }
}
