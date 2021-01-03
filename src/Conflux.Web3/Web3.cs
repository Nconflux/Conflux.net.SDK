using System;
using System.Net.Http.Headers;
using Common.Logging;
using Conflux.BlockchainProcessing.Services;
using Conflux.Contracts;
using Conflux.Contracts.Services;
using Conflux.JsonRpc.Client;
using Conflux.RPC;
using Conflux.RPC.Accounts;
using Conflux.RPC.TransactionManagers;
using Conflux.Signer;
using Conflux.Util;

namespace Conflux.Web3
{
    public class Web3 : IWeb3
    {
        private static readonly AddressUtil addressUtil = new AddressUtil();
        private static readonly Sha3Keccack sha3Keccack = new Sha3Keccack();

        public Web3(IClient client)
        {
            Client = client;
            InitialiseInnerServices();
            IntialiseDefaultGasAndGasPrice();
        }

        public Web3(IAccount account, IClient client) : this(client)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public Web3(string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null)
        {
            IntialiseDefaultRpcClient(url, log, authenticationHeader);
            InitialiseInnerServices();
            IntialiseDefaultGasAndGasPrice();
        }

        public Web3(IAccount account, string url = @"http://localhost:8545/", ILog log = null, AuthenticationHeaderValue authenticationHeader = null) : this(url, log, authenticationHeader)
        {
            TransactionManager = account.TransactionManager;
            TransactionManager.Client = Client;
        }

        public ITransactionManager TransactionManager
        {
            get => Cfx.TransactionManager;
            set => Cfx.TransactionManager = value;
        }

        public static UnitConversion Convert { get; } = new UnitConversion();

        public static TransactionSigner OfflineTransactionSigner { get; } = new TransactionSigner();

        public IClient Client { get; private set; }

        public ICfxApiContractService Cfx { get; private set; }
        public IShhApiService Shh { get; private set; }
        public INetApiService Net { get; private set; }
        public IPersonalApiService Personal { get; private set; }
        public IBlockchainProcessingService Processing { get; private set; }

        private void IntialiseDefaultGasAndGasPrice()
        {
            TransactionManager.DefaultGas = Transaction.DEFAULT_GAS_LIMIT;
            TransactionManager.DefaultGasPrice = Transaction.DEFAULT_GAS_PRICE;
        }

        public static string GetAddressFromPrivateKey(string privateKey)
        {
            return EthECKey.GetPublicAddress(privateKey);
        }

        public static bool IsChecksumAddress(string address)
        {
            return addressUtil.IsChecksumAddress(address);
        }

        public static string Sha3(string value)
        {
            return sha3Keccack.CalculateHash(value);
        }

        public static string ToChecksumAddress(string address)
        {
            return addressUtil.ConvertToChecksumAddress(address);
        }

        public static string ToValid20ByteAddress(string address)
        {
            return addressUtil.ConvertToValid20ByteAddress(address);
        }

        protected virtual void InitialiseInnerServices()
        {
            Cfx = new EthApiContractService(Client);
            Processing = new BlockchainProcessingService(Cfx);
            Shh = new ShhApiService(Client);
            Net = new NetApiService(Client);
            Personal = new PersonalApiService(Client);
        }

        private void IntialiseDefaultRpcClient(string url, ILog log, AuthenticationHeaderValue authenticationHeader)
        {
            Client = new RpcClient(new Uri(url), authenticationHeader, null, null, log);
        }
    }
}