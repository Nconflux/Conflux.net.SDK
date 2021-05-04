﻿// <copyright file="IBaseEncoder.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;

namespace Conflux.Address
{
    /// <summary>
    /// Basic encoding functionality.
    /// </summary>
    public interface IBaseEncoder
    {
        /// <summary>
        /// Encode a buffer to base-encoded representation.
        /// </summary>
        /// <param name="bytes">Bytes to encode.</param>
        /// <returns>Base16 string.</returns>
        string Encode(ReadOnlySpan<byte> bytes);

        /// <summary>
        /// Decode base-encoded text into bytes.
        /// </summary>
        /// <param name="text">Base16 text.</param>
        /// <returns>Decoded bytes.</returns>
        Span<byte> Decode(ReadOnlySpan<char> text);
    }
}
