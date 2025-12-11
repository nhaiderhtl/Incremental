// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

namespace BigNum;

/// <summary>
/// Centralized suffix constants for number formatting
/// </summary>
public static class SuffixConstants
{
    /// <summary>
    /// Standard suffixes for number formatting (None, K, M, B, T, etc.)
    /// Index 0 = no suffix, Index 1 = K (thousand), Index 2 = M (million), etc.
    /// </summary>
    public static readonly string[] Suffixes =
    [
        "",      // None (0-999)
        "K",     // Thousand (1,000)
        "M",     // Million (1,000,000)
        "B",     // Billion
        "T",     // Trillion
        "Qd",    // Quadrillion
        "Qn",    // Quintillion
        "Sx",    // Sextillion
        "Sp",    // Septillion
        "Oc",    // Octillion
        "No",    // Nonillion
        "De",    // Decillion
        "UDe",   // Undecillion
        "DDe",   // Duodecillion
        "TDe",   // Tredecillion
        "QdDe",  // Quattuordecillion
        "QnDe",  // Quindecillion
        "SxDe",  // Sexdecillion
        "SpDe",  // Septendecillion
        "OcDe",  // Octodecillion
        "NoDe",  // Novemdecillion
        "Vg",    // Vigintillion
        "UVg",   // Unvigintillion
        "DVg",   // Duovigintillion
        "TVg",   // Trevigintillion
        "QdVg",  // Quattuorvigintillion
        "QnVg",  // Quinvigintillion
        "SxVg",  // Sexvigintillion
        "SpVg",  // Septenvigintillion
        "OcVg",  // Octovigintillion
        "NoVg",  // Novemvigintillion
        "Tg"     // Trigintillion
    ];

    /// <summary>
    /// Gets the suffix at the specified index, or empty string if index is out of range
    /// </summary>
    public static string GetSuffix(int index)
    {
        if (index < 0 || index >= Suffixes.Length)
            return "";
        return Suffixes[index];
    }

    /// <summary>
    /// Gets the index of a suffix, or -1 if not found
    /// </summary>
    public static int GetSuffixIndex(string suffix)
    {
        return Array.IndexOf(Suffixes, suffix);
    }

    /// <summary>
    /// Maximum supported suffix index
    /// </summary>
    public static int MaxSuffixIndex => Suffixes.Length - 1;
}

