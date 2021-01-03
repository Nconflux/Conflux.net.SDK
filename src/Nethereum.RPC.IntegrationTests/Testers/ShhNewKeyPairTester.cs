using System;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Shh.KeyPair;
using Xunit;

namespace Nethereum.RPC.Tests.Testers
{
    public class ShhNewKeyPairTester : RPCRequestTester<string>, IRPCRequestTester
    {
        [Fact]
        public async void ShouldReturnTheKeyPair()
        {
            var result = await ExecuteAsync();
            Assert.NotNull(result);
        }

        public override async Task<string> ExecuteAsync(IClient client)
        {
            var shhNewKeyPair = new ShhNewKeyPair(client);
            return await shhNewKeyPair.SendRequestAsync();
        }

        public override Type GetRequestType()
        {
            return typeof(ShhNewKeyPair);
        }
    }
}