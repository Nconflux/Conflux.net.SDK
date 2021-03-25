using System;
using System.Numerics;
using System.Threading.Tasks;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.Model;
using Conflux.RLP;
using Conflux.Util;

namespace Conflux.Signer
{
    public class Transaction : SignedTransactionBase
    {
        public object chainId { get; set; }
        public object v { get; set; }
        public object r { get; set; }
        public object s { get; set; }
        public object epochHeight { get; set; }

        public Transaction(byte[] rawData)
        {
            SimpleRlpSigner = new RLPSigner(rawData, NUMBER_ENCODING_ELEMENTS);
            ValidateValidV(SimpleRlpSigner);
        }

        public Transaction(RLPSigner rlpSigner)
        {
            ValidateValidV(rlpSigner);
            SimpleRlpSigner = rlpSigner;
        }

        private static void ValidateValidV(RLPSigner rlpSigner)
        {
            if (rlpSigner.IsVSignatureForChain())
                throw new Exception("TransactionChainId should be used instead of Transaction");
        }

        public Transaction(byte[] nonce, byte[] gasPrice, byte[] gasLimit, byte[] receiveAddress, byte[] value, byte[] storage, byte[] epoch, byte[] chainId, byte[] data)
        {
            this.Nonce = nonce;
            this.Value = value;
            this.To = receiveAddress;
            this.GasPrice = gasPrice;
            this.Gas = gasLimit;
            this.Storage = storage;
            this.Data = data;
            this.Epoch = epoch;
            this.ChainId = chainId;
        }

        public Transaction(byte[] nonce, byte[] gasPrice, byte[] gasLimit, byte[] receiveAddress, byte[] value, byte[] storage, byte[] epoch, byte[] chainId, byte[] data,
            byte[] r, byte[] s, byte v) : this(nonce, gasPrice, gasLimit, receiveAddress, value, storage, epoch, chainId, data)
        {
        }

        public Transaction(string to, BigInteger amount, BigInteger nonce, BigInteger gasPrice,
            BigInteger gasLimit, BigInteger storageLimit, BigInteger epoch, BigInteger chainId, string data)
        {
            this.Nonce = nonce.ToBytesForRLPEncoding();
            this.Value = amount.ToBytesForRLPEncoding();
            this.To = CIP37.CIP37ToRawBytes(to);
            this.GasPrice = gasPrice.ToBytesForRLPEncoding();
            this.Gas = gasLimit.ToBytesForRLPEncoding();
            this.Storage = storageLimit.ToBytesForRLPEncoding();
            this.Data = data.HexToByteArray();
            this.Epoch = epoch.ToBytesForRLPEncoding();
            this.ChainId = chainId.ToBytesForRLPEncoding();
        }

        //public Transaction(string to, BigInteger amount, BigInteger nonce, BigInteger gasPrice,
        //    BigInteger gasLimit, BigInteger storageLimit, BigInteger epoch, BigInteger chainId, string data) : this(nonce.ToBytesForRLPEncoding(), gasPrice.ToBytesForRLPEncoding(),
        //    gasLimit.ToBytesForRLPEncoding(), CIP37.CIP37ToHex40(to).HexToByteArray(), amount.ToBytesForRLPEncoding(), storageLimit.ToBytesForRLPEncoding(), epoch.ToBytesForRLPEncoding(), chainId.ToBytesForRLPEncoding(), data.HexToByteArray()
        //)
        //{
        //    __amount = amount;
        //    __chainId = chainId;
        //    __epoch = epoch;
        //    __gasLimit = gasLimit;
        //    __gasPrice = gasPrice;
        //    __nonce = nonce;
        //    __storageLimit = storageLimit;

        //    var d = CIP37.CIP37ToHex40(to);
        //   this.To = d.HexToByteArray();
        //    this.Data = data.HexToByteArray();
        //    this.Storage = storageLimit.ToBytesForRLPEncoding();
        //}

        public string ToJsonHex()
        {
            var s = "['{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}']";
            return string.Format(s, Nonce.ToHex(),
                GasPrice.ToHex(), Gas.ToHex(), To.ToHex(), Value.ToHex(), ToHex(Data),
                Signature.V.ToHex(),
                Signature.R.ToHex(),
                Signature.S.ToHex());
        }

        private byte[][] GetElementsInOrder(byte[] nonce, byte[] gasPrice, byte[] gasLimit, byte[] receiveAddress,
            byte[] value, byte[] storage, byte[] epoch, byte[] chainId, byte[] data)
        {
            if (receiveAddress == null)
                receiveAddress = DefaultValues.EMPTY_BYTE_ARRAY;
            //order  nonce, gasPrice, gasLimit, receiveAddress, value, data
            return new[] { nonce, gasPrice, gasLimit, receiveAddress, value, storage, epoch, chainId, data };
        }

        public override void Sign(EthECKey key)
        {
            SimpleRlpSigner.Sign(key);
        }

        public override EthECKey Key => EthECKey.RecoverFromSignature(SimpleRlpSigner.Signature, SimpleRlpSigner.RawHash);

#if !DOTNET35
        public override async Task SignExternallyAsync(IEthExternalSigner externalSigner)
        {
            await externalSigner.SignAsync(this).ConfigureAwait(false);
        }
#endif
    }
}