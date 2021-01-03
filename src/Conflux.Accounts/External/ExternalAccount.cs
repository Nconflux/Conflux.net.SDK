using System;
using System.Numerics;
using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Accounts;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.NonceServices;
using Conflux.RPC.Personal;
using Conflux.RPC.TransactionManagers;
using Conflux.Signer;
using Conflux.Web3.Accounts.Managed;
using Transaction = Conflux.Signer.Transaction;

namespace Conflux.Web3.Accounts
{
    public class ExternalAccount : IAccount
    {
        public IEthExternalSigner ExternalSigner { get; }
        public BigInteger? ChainId { get; }


        public ExternalAccount(IEthExternalSigner externalSigner, BigInteger? chainId = null)
        {
            ExternalSigner = externalSigner;
            ChainId = chainId;
        }

        public ExternalAccount(string address, IEthExternalSigner externalSigner, BigInteger? chainId = null)
        {
            ChainId = chainId;
            Address = address;
            ExternalSigner = externalSigner;
        }

        public async Task InitialiseAsync()
        {
            Address = await ExternalSigner.GetAddressAsync();
        }

        public void InitialiseDefaultTransactionManager(IClient client)
        {
            TransactionManager = new ExternalAccountSignerTransactionManager(client, this, ChainId);
        }

        public string Address { get; protected set; }
        public ITransactionManager TransactionManager { get; protected set; }
        public INonceService NonceService { get; set; }
    }
}