﻿// <copyright file="IEncodingAlphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

namespace Conflux.Address
{
    /// <summary>
    /// Defines basic encoding alphabet.
    /// </summary>
    public interface IEncodingAlphabet
    {
        /// <summary>
        /// Gets the characters in the alphabet.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets the length of the alphabet.
        /// </summary>
        int Length { get; }
    }
}