

using System;
using System.Threading.Tasks;
using Nethereum.Pantheon.RPC.Clique;
using Nethereum.JsonRpc.Client;
using Nethereum.Pantheon.IntegrationTests;
using Nethereum.RPC.Tests.Testers;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Nethereum.Pantheon.Tests.Testers
{

    public class CliqueProposeTester : RPCRequestTester<bool>, IRPCRequestTester
    {
        public override async Task<bool> ExecuteAsync(IClient client)
        {
            var cliquePropose = new CliquePropose(client);
            return await cliquePropose.SendRequestAsync(Settings.GetDefaultAccount(), true);
        }

        public override Type GetRequestType()
        {
            return typeof(CliquePropose);
        }

        [Fact]
        public async void ShouldReturnNotNull()
        {
            var result = await ExecuteAsync();
            Assert.NotNull(result);
        }
    }

}
        