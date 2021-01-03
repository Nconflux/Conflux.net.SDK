﻿using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xunit;

namespace Nethereum.Contracts.IntegrationTests.FiltersEvents
{
    public class FilterBuilderInstantiation
    {
        [Event("Transfer")]
        public class TransferEventDTOBase : IEventDTO
        {
            [Parameter("address", "_from", 1, true)]
            public string From { get; set; }
            [Parameter("address", "_to", 2, true)]
            public string To { get; set; }
            [Parameter("uint256", "_value", 3, false)]
            public BigInteger Value { get; set; }
        }

        public partial class TransferEventDTO : TransferEventDTOBase
        {

        }

        [Fact]

        public void GetEventFilterBuilderFromContractService()
        {
            var web3 = new Web3.Web3();

            //via instance of IEthApiContractService 
            NewFilterInput filterFromContract = web3.Cfx.GetFilterBuilder<TransferEventDTO>()
                .AddTopic(t => t.From, "")
                .AddTopic(t => t.To, "")
                .Build(contractAddresses: new string[] { "", "" });

            Assert.NotNull(filterFromContract);

        }

        [Fact]

        public void GetEventFilterBuilderFromEvent()
        {
            var web3 = new Web3.Web3();

            var transferEvent = web3.Cfx.GetEvent<TransferEventDTO>();

            //from instance of an Event
            NewFilterInput filterFromEvent = transferEvent.GetFilterBuilder()
                .AddTopic(t => t.From, "")
                .AddTopic(t => t.To, "")
                .Build(contractAddresses: new string[] { "", "" });

            Assert.NotNull(filterFromEvent);

        }

        [Fact]

        public void GetEventFilterBuilderFromEventDto()
        {
            var transferEventDto = new TransferEventDTO();

            //from instance of an Event
            NewFilterInput filterFromEventDto = transferEventDto.GetFilterBuilder()
                .AddTopic(t => t.From, "")
                .AddTopic(t => t.To, "")
                .Build(contractAddresses: new string[] { "", "" });

            Assert.NotNull(filterFromEventDto);

        }
    }
}
