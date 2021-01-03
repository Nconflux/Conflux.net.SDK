﻿using System.Linq;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.Model;
using Conflux.RPC.Eth.DTOs;
using Conflux.Hex;

namespace Conflux.RPC.ModelFactories
{
    public class BlockHeaderRPCFactory
    {
        public static BlockHeader FromRPC(Block rpcBlock, bool mixHasAndhNonceInSealFields = false)
        {
            var blockHeader = new BlockHeader();
            blockHeader.BlockNumber = rpcBlock.Number;
            blockHeader.Coinbase = rpcBlock.Miner;
            blockHeader.Difficulty = rpcBlock.Difficulty;
            blockHeader.ExtraData = rpcBlock.ExtraData.HexToByteArray();
            blockHeader.GasLimit = (long)rpcBlock.GasLimit.Value;
            blockHeader.GasUsed = (long)rpcBlock.GasUsed.Value;
            blockHeader.LogsBloom = rpcBlock.LogsBloom.HexToByteArray();
            blockHeader.ParentHash = rpcBlock.ParentHash.HexToByteArray();
            blockHeader.ReceiptHash = rpcBlock.ReceiptsRoot.HexToByteArray();
            blockHeader.StateRoot = rpcBlock.StateRoot.HexToByteArray();
            blockHeader.Timestamp = (long)rpcBlock.Timestamp.Value;
            blockHeader.TransactionsHash = rpcBlock.TransactionsRoot.HexToByteArray();
            blockHeader.UnclesHash = rpcBlock.Sha3Uncles.HexToByteArray();

            if (mixHasAndhNonceInSealFields && rpcBlock.SealFields != null && rpcBlock.SealFields.Length >= 2)
            {
                blockHeader.MixHash = EnsureMixHashWithoutRLPSizePrefix(rpcBlock.SealFields[0].HexToByteArray());
                blockHeader.Nonce = EnsureNonceWithoutRLPSizePrefix(rpcBlock.SealFields[1].HexToByteArray());
            }
            else
            {
                blockHeader.MixHash = rpcBlock.MixHash.HexToByteArray();
                blockHeader.Nonce = rpcBlock.Nonce.HexToByteArray();
            }
            return blockHeader;
        }

        public static byte[] EnsureMixHashWithoutRLPSizePrefix(byte[] mixHash)
        {
            if (mixHash.Length == 33 && mixHash[0] == 0xA0) return mixHash.Skip(1).ToArray();
            return mixHash;
        }

        public static byte[] EnsureNonceWithoutRLPSizePrefix(byte[] nonce)
        {
            if (nonce.Length == 9 && nonce[0] == 0x88) return nonce.Skip(1).ToArray();
            return nonce;
        }
    }
}