using System;
using System.Numerics;
using System.Threading.Tasks;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.KeyStore;
using Conflux.RPC.Accounts;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Transactions;
using Conflux.RPC.NonceServices;
using Conflux.RPC.TransactionManagers;
using Conflux.Signer;
using Conflux.Util;
using Transaction = Conflux.Signer.Transaction;

namespace Conflux.Web3.Accounts
{
    public class AccountSignerTransactionManager : TransactionManagerBase
    {
        private readonly AccountOfflineTransactionSigner _transactionSigner;
        public BigInteger? ChainId { get; private set; }

        public AccountSignerTransactionManager(IClient rpcClient, Account account, BigInteger? chainId = null)
        {
            ChainId = chainId;
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Client = rpcClient;
            _transactionSigner = new AccountOfflineTransactionSigner();
        }


        public AccountSignerTransactionManager(IClient rpcClient, string privateKey, BigInteger? chainId = null)
        {
            ChainId = chainId;
            if (privateKey == null) throw new ArgumentNullException(nameof(privateKey));
            Client = rpcClient;
            Account = new Account(privateKey);
            Account.NonceService = new InMemoryNonceService(Account.Address, rpcClient);
            _transactionSigner = new AccountOfflineTransactionSigner();
        }

        public AccountSignerTransactionManager(string privateKey, BigInteger? chainId = null) : this(null, privateKey, chainId)
        {

        }

        public override BigInteger DefaultGas { get; set; } = Transaction.DEFAULT_GAS_LIMIT;


        public override Task<string> SendTransactionAsync(TransactionInput transactionInput)
        {
            if (transactionInput == null) throw new ArgumentNullException(nameof(transactionInput));
            return SignAndSendTransactionAsync(transactionInput);
        }

        public override Task<string> SignTransactionAsync(TransactionInput transaction)
        {
            return SignTransactionRetrievingNextNonceAsync(transaction);
        }

        public string SignTransaction(TransactionInput transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            SetDefaultGasPriceAndCostIfNotSet(transaction);
            return _transactionSigner.SignTransaction((Account)Account, transaction, ChainId);
        }

        protected async Task<string> SignTransactionRetrievingNextNonceAsync(TransactionInput transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (!transaction.From.IsTheSameAddress(Account.Address))
                throw new Exception("Invalid account used signing");
            //var nonce = await GetNonceAsync(transaction).ConfigureAwait(false);
            //transaction.Nonce = nonce;
            var gasPrice = await GetGasPriceAsync(transaction).ConfigureAwait(false);
            transaction.GasPrice = gasPrice;
            //transaction.GasPrice = new HexBigInteger(new BigInteger(1000000));
            transaction.Gas = new HexBigInteger(new BigInteger(8000000));
            return SignTransaction(transaction);
        }

        public async Task<HexBigInteger> GetNonceAsync(TransactionInput transaction)
        {
            if (Client == null) throw new NullReferenceException("Client not configured");
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            var nonce = transaction.Nonce;
            if (nonce == null)
            {
                if (Account.NonceService == null)
                    Account.NonceService = new InMemoryNonceService(Account.Address, Client);
                Account.NonceService.Client = Client;
                //nonce = "0x00";// await Account.NonceService.GetNextNonceAsync().ConfigureAwait(false);
            }
            return new HexBigInteger("0x00");
        }

        private async Task<string> SignAndSendTransactionAsync(TransactionInput transaction)
        {
            if (Client == null) throw new NullReferenceException("Client not configured");
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (!transaction.From.IsTheSameAddress(Account.Address))
                throw new Exception("Invalid account used signing");

            var ethSendTransaction = new EthSendRawTransaction(Client);
            //transaction.StorageLimit = new HexBigInteger("0x2222222");
            var signedTransaction = await SignTransactionRetrievingNextNonceAsync(transaction).ConfigureAwait(false);
            return await ethSendTransaction.SendRequestAsync(signedTransaction.EnsureHexPrefix()).ConfigureAwait(false);
        }
    }
}
