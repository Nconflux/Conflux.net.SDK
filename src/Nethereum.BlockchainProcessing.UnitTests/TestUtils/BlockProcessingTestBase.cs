﻿using Nethereum.BlockchainProcessing.UnitTests.TestUtils;
using Nethereum.Web3;

namespace Nethereum.BlockchainProcessing.UnitTests.TestUtils
{
    public class ProcessingTestBase
    {
        protected Web3Mock Web3Mock;
        protected IWeb3 Web3 => Web3Mock.Web3;

        public ProcessingTestBase()
        {
            Web3Mock = new Web3Mock();
        }

    }
}
