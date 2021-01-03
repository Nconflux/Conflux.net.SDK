﻿using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;
using Nethereum.XUnitEthereumClients;
using Xunit;

namespace Nethereum.Accounts.IntegrationTests
{
    [Collection(EthereumClientIntegrationFixture.ETHEREUM_CLIENT_COLLECTION_DEFAULT)]
    public class TransactionRawRecovery
    {
        private readonly EthereumClientIntegrationFixture _ethereumClientIntegrationFixture;

        public TransactionRawRecovery(EthereumClientIntegrationFixture ethereumClientIntegrationFixture)
        {
            _ethereumClientIntegrationFixture = ethereumClientIntegrationFixture;
        }

        [Fact]
        public async void ShouldRecoverRawTransactionFromRPCTransactionAndAccountSender()
        {

            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var toAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe";
           
            var transactionManager = web3.TransactionManager;
            var fromAddress = transactionManager?.Account?.Address;

            //Sending transaction
            var transactionInput = EtherTransferTransactionInputBuilder.CreateTransactionInput(fromAddress, toAddress, 1.11m, 2);
            //Raw transaction signed
            var rawTransaction = await transactionManager.SignTransactionAsync(transactionInput);
            var txnHash = await web3.Cfx.Transactions.SendRawTransaction.SendRequestAsync(rawTransaction);
            //Getting the transaction from the chain
            var transactionRpc = await web3.Cfx.Transactions.GetTransactionByHash.SendRequestAsync(txnHash);
            
            //Using the transanction from RPC to build a txn for signing / signed
            var transaction = TransactionFactory.CreateTransaction(transactionRpc.To, transactionRpc.Gas, transactionRpc.GasPrice, transactionRpc.Value, transactionRpc.Input, transactionRpc.Nonce,
                transactionRpc.R, transactionRpc.S, transactionRpc.V);
            
            //Get the raw signed rlp recovered
            var rawTransactionRecovered = transaction.GetRLPEncoded().ToHex();
            
            //Get the account sender recovered
            var accountSenderRecovered = string.Empty;
            if (transaction is TransactionChainId)
            {
                var txnChainId = transaction as TransactionChainId;
                accountSenderRecovered = EthECKey.RecoverFromSignature(transaction.Signature, transaction.RawHash, txnChainId.GetChainIdAsBigInteger()).GetPublicAddress();
            }
            else
            {
                accountSenderRecovered = EthECKey.RecoverFromSignature(transaction.Signature, transaction.RawHash).GetPublicAddress();
            }

            Assert.Equal(rawTransaction, rawTransactionRecovered);
            Assert.Equal(web3.TransactionManager.Account.Address, accountSenderRecovered);
        }
    }
}