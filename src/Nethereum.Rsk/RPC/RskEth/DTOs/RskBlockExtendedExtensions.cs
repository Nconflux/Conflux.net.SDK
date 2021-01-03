﻿using System.Numerics;

namespace Conflux.Rsk.RPC.RskEth.DTOs
{
    public static class RskBlockExtendedExtensions
    {
        public static BigInteger GetMinimumGasPriceAsBigInteger(this IRskBlockExtended rskBlock)
        {
            return string.IsNullOrEmpty(rskBlock.MinimumGasPriceString) ? 0 : BigInteger.Parse(rskBlock.MinimumGasPriceString);
        }
    }
}