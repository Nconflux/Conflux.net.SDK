﻿using System.Net.Http.Headers;
using Common.Logging;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Accounts;

namespace Conflux.Pantheon
{
    public class Web3Pantheon : Web3.Web3
    {
        public Web3Pantheon(IClient client) : base(client)
        {
        }

        public Web3Pantheon(string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null) : base(url, log, authenticationHeader)
        {
        }

        public Web3Pantheon(IAccount account, IClient client) : base(account, client)
        {
        }

        public Web3Pantheon(IAccount account, string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null) : base(account, url, log, authenticationHeader)
        {
        }

        public IAdminApiService Admin { get; private set; }

        public IDebugApiService Debug { get; private set; }

        public IMinerApiService Miner { get; private set; }

        public ICliqueApiService Clique { get; private set; }

        public IIbftApiService Ibft { get; private set; }

        public IPermissioningApiService Permissioning { get; private set; }

        public IEeaApiService Eea { get; private set; }

        public ITxPoolApiService TxPool { get; private set; }

 

        protected override void InitialiseInnerServices()
        {
            base.InitialiseInnerServices();
            Miner = new MinerApiService(Client);
            Debug = new DebugApiService(Client);
            Admin = new AdminApiService(Client);
            Clique = new CliqueApiService(Client);
            Ibft = new IbftApiService(Client);
            Permissioning = new PermissioningApiService(Client);
            Eea = new EeaApiService(Client);
            TxPool = new TxPoolApiService(Client);
        }
    }
}