using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth;
using Conflux.RPC.Eth.DTOs;
using Conflux.Util;

namespace Conflux.RPC.TransactionManagers
{
#if !DOTNET35
    public class EtherTransferService : IEtherTransferService
    {
        private readonly ITransactionManager _transactionManager;

        public EtherTransferService(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager ?? throw new ArgumentNullException(nameof(transactionManager));
        }

        public Task<string> TransferEtherAsync(string toAddress, decimal etherAmount, decimal? gasPriceGwei = null, BigInteger? gas = null, BigInteger? nonce = null)
        {
            var fromAddress = _transactionManager?.Account?.Address;
            var transactionInput = EtherTransferTransactionInputBuilder.CreateTransactionInput(fromAddress, toAddress, etherAmount, gasPriceGwei, gas, nonce);
            return _transactionManager.SendTransactionAsync(transactionInput);
        }

        public Task<TransactionReceipt> TransferEtherAndWaitForReceiptAsync(string toAddress, decimal etherAmount, decimal? gasPriceGwei = null, HexBigInteger epochNumber = null, HexBigInteger nextNonce = null, BigInteger? gas = null, CancellationTokenSource tokenSource = null, BigInteger? nonce = null)
        {
            var fromAddress = _transactionManager?.Account?.Address;
            var transactionInput = EtherTransferTransactionInputBuilder.CreateTransactionInput(fromAddress, toAddress, etherAmount, gasPriceGwei, gas, nonce, epochNumber,nextNonce);
            return _transactionManager.SendTransactionAndWaitForReceiptAsync(transactionInput, tokenSource);
        }

        public async Task<decimal> CalculateTotalAmountToTransferWholeBalanceInEther(string address, decimal gasPriceGwei, BigInteger? gas = null)
        {
            var ethGetBalance = new EthGetBalance(_transactionManager.Client);
            var currentBalance = await ethGetBalance.SendRequestAsync(address);
            var gasPrice = UnitConversion.Convert.ToWei(gasPriceGwei, UnitConversion.EthUnit.Gwei);
            var gasAmount = gas ?? _transactionManager.DefaultGas;

            var totalAmount = currentBalance.Value - (gasAmount * gasPrice);
            if (totalAmount <= 0) throw new Exception("Insufficient balance to make a transfer");
            return UnitConversion.Convert.FromDrip(totalAmount);
        }

        public async Task<EstimatedGasAndCollateral> EstimateGasAndCollateralAsync(string toAddress, decimal etherAmount)
        {
            var fromAddress = _transactionManager?.Account?.Address;
            var callInput = (CallInput)EtherTransferTransactionInputBuilder.CreateTransactionInput(fromAddress, toAddress, etherAmount);
            var hexEstimate = await _transactionManager.EstimatedGasAndCollateralAsync(callInput);
            return hexEstimate;
        }
    }
#endif
}