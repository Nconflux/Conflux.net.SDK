﻿namespace Conflux.ABI.Decoders
{
    public interface ICustomRawDecoder<T>
    {
        T Decode(byte[] output);
    }
}