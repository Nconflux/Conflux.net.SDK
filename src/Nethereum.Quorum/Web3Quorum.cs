﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Threading.Tasks;
using Common.Logging;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.Quorum.RPC.Interceptors;
using Conflux.Quorum.RPC.Services;
using Conflux.RPC.Accounts;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.NonceServices;
using Conflux.RPC.TransactionManagers;
using Conflux.Web3.Accounts;

namespace Conflux.Quorum
{

    public class Web3Quorum : Web3.Web3, IWeb3Quorum
    {
        public Web3Quorum(IClient client, string accountAddress) :base(client)
        {
            var account = new UnlockedAccount(accountAddress);
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public Web3Quorum(IClient client, UnlockedAccount account) : base(client)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public Web3Quorum(UnlockedAccount account, string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null) : base(url, log, authenticationHeader)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public Web3Quorum(QuorumAccount account, IClient client, string privateUrl) : base(account, client)
        {
            ((QuorumTransactionManager) TransactionManager).PrivateUrl = privateUrl;
        }

        public Web3Quorum(QuorumAccount account, string privateUrl, string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null) : base(account, url, log, authenticationHeader)
        {
            ((QuorumTransactionManager)TransactionManager).PrivateUrl = privateUrl;
        }

        public Web3Quorum(string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null) : base(url, log, authenticationHeader)
        {

        }

        protected override void InitialiseInnerServices()
        {
            base.InitialiseInnerServices();
            Quorum = new QuorumChainService(Client);
            base.TransactionManager.DefaultGasPrice = 0;
        }

        public IQuorumChainService Quorum { get; private set; }

        public List<string> PrivateFor { get; private set; }
        public string PrivateFrom { get; private set; }

        public void SetPrivateRequestParameters(IEnumerable<string> privateFor, string privateFrom = null)
        {
            var list = privateFor.ToList();
            if(privateFor == null || list.Count == 0) throw new ArgumentNullException(nameof(privateFor));
            this.PrivateFor = list;
            this.PrivateFrom = privateFrom;
            this.Client.OverridingRequestInterceptor = new PrivateForInterceptor(list, privateFrom);

            if (TransactionManager is QuorumTransactionManager manager)
            {
                manager.PrivateFor = PrivateFor;
                manager.PrivateFrom = PrivateFrom;
            }
        }

        public void ClearPrivateForRequestParameters()
        {
            if (Client.OverridingRequestInterceptor is PrivateForInterceptor)
            {
                Client.OverridingRequestInterceptor = null;
            }

            PrivateFor = null;
            PrivateFrom = null;

            if (TransactionManager is QuorumTransactionManager manager)
            {
                manager.PrivateFor = null;
                manager.PrivateFrom = null;
            }
        }
    }
}
