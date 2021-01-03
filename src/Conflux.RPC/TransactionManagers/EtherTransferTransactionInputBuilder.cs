using System;
using System.Numerics;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using Conflux.Util;

namespace Conflux.RPC.TransactionManagers
{
    public class EtherTransferTransactionInputBuilder
    {
        public static TransactionInput CreateTransactionInput(string fromAddress, string toAddress, decimal etherAmount, decimal? gasPriceGwei = null, BigInteger? gas = null, BigInteger? nonce = null, HexBigInteger epochNumber = null, HexBigInteger nextNonce = null)
        {
            if (string.IsNullOrEmpty(toAddress)) throw new ArgumentNullException(nameof(toAddress));
            if (etherAmount <= 0) throw new ArgumentOutOfRangeException(nameof(etherAmount));
            if (gasPriceGwei <= 0) throw new ArgumentOutOfRangeException(nameof(gasPriceGwei));

            var transactionInput = new TransactionInput()
            {
                To = toAddress,
                From = fromAddress,
                GasPrice = gasPriceGwei == null ? null : new HexBigInteger(UnitConversion.Convert.ToWei(gasPriceGwei.Value, UnitConversion.EthUnit.Gwei)),
                Value = new HexBigInteger(UnitConversion.Convert.ToWei(etherAmount)),
                Gas = gas == null ? null : new HexBigInteger(gas.Value),
                Nonce =nextNonce,
                EpochNumber = epochNumber,
            };
            return transactionInput;
        }
    }
}