// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Incremental;

public class GameViewModel : INotifyPropertyChanged
{
    private readonly GameLogic _gameLogic;

    public event PropertyChangedEventHandler? PropertyChanged;

    // Properties for UI binding
    public string CashDisplay => GameLogic.FormatCash(_gameLogic.Cash);
    public string BaseCashDisplay => GameLogic.FormatCash(_gameLogic.BaseCash);
    public string MultiplierDisplay => $"×{_gameLogic.Multiplier:0.##}";
    public string IncomeDisplay => GameLogic.FormatCash(_gameLogic.BaseCash * _gameLogic.Multiplier);
    public string Plus1CostDisplay => GameLogic.FormatCash(_gameLogic.Plus1Cost);
    public string X2CostDisplay => GameLogic.FormatCash(_gameLogic.X2Cost);

    public bool CanAffordPlus1 => _gameLogic.Cash >= _gameLogic.Plus1Cost;
    public bool CanAffordX2 => _gameLogic.Cash >= _gameLogic.X2Cost;

    public GameViewModel()
    {
        _gameLogic = new GameLogic();
        _gameLogic.CashChanged += OnGameStateChanged;
        _gameLogic.UpgradeCostsChanged += OnGameStateChanged;
    }

    public void GainCash()
    {
        _gameLogic.GainCash();
    }

    public void BuyPlus1()
    {
        _gameLogic.TryBuyPlus1();
    }

    public void BuyX2()
    {
        _gameLogic.TryBuyX2();
    }

    private void OnGameStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CashDisplay));
        OnPropertyChanged(nameof(BaseCashDisplay));
        OnPropertyChanged(nameof(MultiplierDisplay));
        OnPropertyChanged(nameof(IncomeDisplay));
        OnPropertyChanged(nameof(Plus1CostDisplay));
        OnPropertyChanged(nameof(X2CostDisplay));
        OnPropertyChanged(nameof(CanAffordPlus1));
        OnPropertyChanged(nameof(CanAffordX2));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}