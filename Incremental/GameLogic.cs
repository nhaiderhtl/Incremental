// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System;
using BigNum;

namespace Incremental;

public class GameLogic
{
    // Game state
    private double _cash;
    private double _baseCash = 1;
    private double _multiplier = 1;

    // Upgrade costs
    private double _x2Cost = 100;
    private double _plus1Cost = 10;


    // Events for UI updates
    public event EventHandler? CashChanged;
    public event EventHandler? UpgradeCostsChanged;

    // Properties for reading game state
    public double Cash => _cash;
    public double BaseCash => _baseCash;
    public double Multiplier => _multiplier;
    public double X2Cost => _x2Cost;
    public double Plus1Cost => _plus1Cost;

    public GameLogic()
    {
        _cash = 0;
    }

    /// <summary>
    /// Gain cash based on current base cash and multiplier
    /// </summary>
    public void GainCash()
    {
        _cash += _baseCash * _multiplier;
        OnCashChanged();
    }

    /// <summary>
    /// Try to buy the 2x multiplier upgrade
    /// </summary>
    /// <returns>True if purchase was successful, false if not enough cash</returns>
    public bool TryBuyX2()
    {
        if (_cash < _x2Cost) return false;

        _cash -= _x2Cost;
        _multiplier *= 2;
        _x2Cost *= 1.5;

        OnCashChanged();
        OnUpgradeCostsChanged();
        return true;
    }

    /// <summary>
    /// Try to buy the +1 base cash upgrade
    /// </summary>
    /// <returns>True if purchase was successful, false if not enough cash</returns>
    public bool TryBuyPlus1()
    {
        if (_cash < _plus1Cost) return false;

        _cash -= _plus1Cost;
        _baseCash += 1;
        _plus1Cost *= 2.5;

        OnCashChanged();
        OnUpgradeCostsChanged();
        return true;
    }

    /// <summary>
    /// Format cash value with suffixes
    /// </summary>
    public static string FormatCash(double value)
    {
        if (value < 1000)
        {
            return value.ToString("0");
        }

        int suffixIndex = 0;
        double displayValue = value;

        while (displayValue >= 1000 && suffixIndex < SuffixConstants.MaxSuffixIndex)
        {
            displayValue /= 1000;
            suffixIndex++;
        }

        return suffixIndex == 0 ? displayValue.ToString("0") : $"{displayValue:0.00} {SuffixConstants.GetSuffix(suffixIndex)}";
    }

    protected virtual void OnCashChanged()
    {
        CashChanged?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnUpgradeCostsChanged()
    {
        UpgradeCostsChanged?.Invoke(this, EventArgs.Empty);
    }
}