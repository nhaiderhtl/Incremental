// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Incremental;

namespace UI;

public partial class MainWindow : Window
{
    private readonly GameViewModel _viewModel;
    private readonly DispatcherTimer _incomeTimer;
    
    // UI Elements
    private TextBlock _cashDisplay = null!;
    private TextBlock _baseCashDisplay = null!;
    private TextBlock _multiplierDisplay = null!;
    private TextBlock _incomeDisplay = null!;
    private TextBlock _plus1CostDisplay = null!;
    private TextBlock _x2CostDisplay = null!;
    private Button _btnPlus1 = null!;
    private Button _btnX2 = null!;

    // Color brushes for affordability
    private static readonly IBrush AffordableColor = new SolidColorBrush(Color.Parse("#3fb950"));
    private static readonly IBrush UnaffordableColor = new SolidColorBrush(Color.Parse("#f85149"));

    public MainWindow()
    {
        InitializeComponent();
        
        // Initialize ViewModel
        _viewModel = new GameViewModel();
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        
        // Setup timer for passive income
        _incomeTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _incomeTimer.Tick += (_, _) => _viewModel.GainCash();
        _incomeTimer.Start();
        
        // Get UI element references
        _cashDisplay = this.FindControl<TextBlock>("CashDisplay")!;
        _baseCashDisplay = this.FindControl<TextBlock>("BaseCashDisplay")!;
        _multiplierDisplay = this.FindControl<TextBlock>("MultiplierDisplay")!;
        _incomeDisplay = this.FindControl<TextBlock>("IncomeDisplay")!;
        _plus1CostDisplay = this.FindControl<TextBlock>("Plus1CostDisplay")!;
        _x2CostDisplay = this.FindControl<TextBlock>("X2CostDisplay")!;
        _btnPlus1 = this.FindControl<Button>("BtnPlus1")!;
        _btnX2 = this.FindControl<Button>("BtnX2")!;
        
        // Initial UI update
        UpdateUI();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnBuyPlus1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _viewModel.BuyPlus1();
    }

    private void OnBuyX2(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _viewModel.BuyX2();
    }

    private void OnExitGame(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update displays
        _cashDisplay.Text = _viewModel.CashDisplay;
        _baseCashDisplay.Text = _viewModel.BaseCashDisplay;
        _multiplierDisplay.Text = _viewModel.MultiplierDisplay;
        _incomeDisplay.Text = _viewModel.IncomeDisplay;
        _plus1CostDisplay.Text = _viewModel.Plus1CostDisplay;
        _x2CostDisplay.Text = _viewModel.X2CostDisplay;
        
        // Update button states
        _btnPlus1.IsEnabled = _viewModel.CanAffordPlus1;
        _btnX2.IsEnabled = _viewModel.CanAffordX2;
        
        // Update cost colors based on affordability
        _plus1CostDisplay.Foreground = _viewModel.CanAffordPlus1 ? AffordableColor : UnaffordableColor;
        _x2CostDisplay.Foreground = _viewModel.CanAffordX2 ? AffordableColor : UnaffordableColor;
    }
}



