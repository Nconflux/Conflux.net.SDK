using System;
using System.Numerics;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using Conflux.Signer;
using Conflux.Util;

namespace Conflux.Web3.Accounts
{
    public class AccountOfflineTransactionSigner
    {
        private readonly TransactionSigner _transactionSigner;

        public AccountOfflineTransactionSigner(TransactionSigner transactionSigner)
        {
            _transactionSigner = transactionSigner;
        }

        public AccountOfflineTransactionSigner()
        {
            _transactionSigner = new TransactionSigner();
        }

        public string SignTransaction(Account account, TransactionInput transaction, BigInteger? chainId = null)
        {

            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (string.IsNullOrWhiteSpace(transaction.From))
            {
                transaction.From = account.Address;
            }
            else if (!transaction.From.IsTheSameAddress(account.Address))
            {
                throw new Exception("Invalid account used for signing, does not match the transaction input");
            }

            var nonce = transaction.Nonce;
            if (nonce == null) throw new ArgumentNullException(nameof(transaction), "Transaction nonce has not been set");

            string signedTransaction = _transactionSigner.SignTransaction(
                account.PrivateKey, transaction.To,
                transaction.Value, transaction.Nonce,
                transaction.GasPrice, transaction.Gas,
                transaction.StorageLimit, transaction.Data,
                transaction.EpochNumber, chainId.Value);
            return signedTransaction;
        }
    }
}