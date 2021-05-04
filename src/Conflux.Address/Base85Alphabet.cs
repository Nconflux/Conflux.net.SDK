﻿// <copyright file="Base85Alphabet.cs" company="Sedat Kapanoglu">
// Copyright (c) 2014-2019 Sedat Kapanoglu
// Licensed under Apache-2.0 License (see LICENSE.txt file for details)
// </copyright>

using System;
using System.Threading;

namespace Conflux.Address
{
    /// <summary>
    /// Base85 Alphabet.
    /// </summary>
    public sealed class Base85Alphabet : EncodingAlphabet
    {
        private static readonly Lazy<Base85Alphabet> z85 = new Lazy<Base85Alphabet>(() => new Base85Alphabet(
                "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#"));

        private static readonly Lazy<Base85Alphabet> ascii85 = new Lazy<Base85Alphabet>(() => new Base85Alphabet(
                "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstu",
                allZeroShortcut: 'z',
                allSpaceShortcut: 'y'));

        /// <summary>
        /// Initializes a new instance of the <see cref="Base85Alphabet"/> class
        /// using custom settings.
        /// </summary>
        /// <param name="alphabet">Alphabet to use.</param>
        /// <param name="allZeroShortcut">Character to substitute for all zero.</param>
        /// <param name="allSpaceShortcut">Character to substitute for all space.</param>
        public Base85Alphabet(
            string alphabet,
            char? allZeroShortcut = null,
            char? allSpaceShortcut = null)
            : base(85, alphabet)
        {
            this.AllZeroShortcut = allZeroShortcut;
            this.AllSpaceShortcut = allSpaceShortcut;
        }

        /// <summary>
        /// Gets ZeroMQ Z85 Alphabet.
        /// </summary>
        public static Base85Alphabet Z85 => z85.Value;

        /// <summary>
        /// Gets Adobe Ascii85 Alphabet (each character is directly produced by raw value + 33),
        /// also known as "btoa" encoding.
        /// </summary>
        public static Base85Alphabet Ascii85 => ascii85.Value;

        /// <summary>
        /// Gets the character to be used for "all zeros".
        /// </summary>
        public char? AllZeroShortcut { get; }

        /// <summary>
        /// Gets the character to be used for "all spaces".
        /// </summary>
        public char? AllSpaceShortcut { get; }

        /// <summary>
        /// Gets a value indicating whether the alphabet uses one of shortcut characters for all spaces
        /// or all zeros.
        /// </summary>
        public bool HasShortcut => AllSpaceShortcut.HasValue || AllZeroShortcut.HasValue;
    }
}
