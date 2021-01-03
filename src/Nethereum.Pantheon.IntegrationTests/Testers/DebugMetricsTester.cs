

using System;
using System.Threading.Tasks;
using Nethereum.Pantheon.RPC.Debug;
using Nethereum.JsonRpc.Client;
using Nethereum.Pantheon.IntegrationTests;
using Nethereum.RPC.Tests.Testers;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Nethereum.Pantheon.Tests.Testers
{

    public class DebugMetricsTester : RPCRequestTester<JObject>, IRPCRequestTester
    {
        public override async Task<JObject> ExecuteAsync(IClient client)
        {
            var debugMetrics = new DebugMetrics(client);
            return await debugMetrics.SendRequestAsync();
        }

        public override Type GetRequestType()
        {
            return typeof(DebugMetrics);
        }

        [Fact]
        public async void ShouldReturnNotNull()
        {
            var result = await ExecuteAsync();
            Assert.NotNull(result);
        }
    }

}
        