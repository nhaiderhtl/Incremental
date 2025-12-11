// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System.Globalization;

namespace BigNum;

public class BigNum : IEquatable<BigNum>
{
    public double Number { get; set; }
    public string Suffix { get; set; }

    public BigNum(double number, string suffix)
    {
        Number = number;
        Suffix = suffix;
    }

    public BigNum(string number)
    {
        var bigNum = ToBigNum(number);
        Number = bigNum.Number;
        Suffix = bigNum.Suffix;
    }

    public static BigNum ToBigNum(string number)
    {
        var parts = number.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 1)
        {
            // No suffix, just a number
            var num = double.Parse(parts[0], CultureInfo.InvariantCulture);
            return new BigNum(num, "");
        }
        else if (parts.Length == 2)
        {
            // Number with suffix
            var num = double.Parse(parts[0], CultureInfo.InvariantCulture);
            var suffix = parts[1];
            return new BigNum(num, suffix);
        }
        else
        {
            throw new Exception("Invalid number");
        }
    }

    public override string ToString()
    {
        var result = Number.ToString(CultureInfo.InvariantCulture);
        if (Suffix != "") result += " " + Suffix;
        return result;
    }

    public static BigNum operator +(BigNum a, BigNum b)
    {
        // Get suffix indices
        int aIndex = SuffixConstants.GetSuffixIndex(a.Suffix);
        int bIndex = SuffixConstants.GetSuffixIndex(b.Suffix);
        
        // Normalize to the higher suffix
        int resultIndex = Math.Max(aIndex, bIndex);
        var aValue = a.Number;
        var bValue = b.Number;

        // Convert a to result suffix
        while (aIndex < resultIndex)
        {
            aValue /= 1000;
            aIndex++;
        }

        // Convert b to result suffix
        while (bIndex < resultIndex)
        {
            bValue /= 1000;
            bIndex++;
        }

        var amount = aValue + bValue;

        // Handle overflow to next suffix
        while (amount >= 1000 && resultIndex < SuffixConstants.MaxSuffixIndex)
        {
            amount /= 1000;
            resultIndex++;
        }

        return new BigNum(amount, SuffixConstants.GetSuffix(resultIndex));
    }

    public static (BigNum, bool) operator -(BigNum a, BigNum b)
    {
        // Get suffix indices
        int aIndex = SuffixConstants.GetSuffixIndex(a.Suffix);
        int bIndex = SuffixConstants.GetSuffixIndex(b.Suffix);
        
        // Check if a < b (can't subtract)
        if (aIndex < bIndex || (aIndex == bIndex && a.Number < b.Number))
        {
            return (new BigNum(0, ""), false);
        }

        // Normalize to the higher suffix
        int resultIndex = Math.Max(aIndex, bIndex);
        var aValue = a.Number;
        var bValue = b.Number;

        // Convert a to result suffix
        while (aIndex < resultIndex)
        {
            aValue /= 1000;
            aIndex++;
        }

        // Convert b to result suffix
        while (bIndex < resultIndex)
        {
            bValue /= 1000;
            bIndex++;
        }

        var amount = aValue - bValue;

        // If result is exactly zero, keep the suffix
        if (Math.Abs(amount) < 0.0000001)
        {
            return (new BigNum(0, SuffixConstants.GetSuffix(resultIndex)), true);
        }

        // Normalize result - reduce suffix if number is too small
        while (amount < 1 && amount > 0 && resultIndex > 0)
        {
            amount *= 1000;
            resultIndex--;
        }

        return (new BigNum(amount, SuffixConstants.GetSuffix(resultIndex)), true);
    }

    #region Equals

    public override bool Equals(object? obj) =>
        Equals(obj as BigNum);

    public bool Equals(BigNum? other) =>
        other is not null
        && Number.Equals(other.Number)
        && Suffix == other.Suffix;

    public override int GetHashCode() =>
        HashCode.Combine(Number, Suffix);

    public static bool operator ==(BigNum? a, BigNum? b) =>
        a?.Equals(b) ?? b is null;

    public static bool operator !=(BigNum? a, BigNum? b) =>
        !(a == b);

    #endregion
}