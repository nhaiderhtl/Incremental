// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Incremental;

namespace UI;

public partial class MainWindow : Window
{
    private readonly GameViewModel _viewModel;
    private readonly DispatcherTimer _incomeTimer;
    
    private readonly TextBlock _cashDisplay;
    private readonly TextBlock _baseCashDisplay;
    private readonly TextBlock _multiplierDisplay;
    private readonly TextBlock _incomeDisplay;
    private readonly TextBlock _plus1CostDisplay;
    private readonly TextBlock _x2CostDisplay;
    private readonly Button _btnPlus1;
    private readonly Button _btnX2;

    private static readonly IBrush AffordableColor = new SolidColorBrush(Color.Parse("#3fb950"));
    private static readonly IBrush UnaffordableColor = new SolidColorBrush(Color.Parse("#f85149"));

    public MainWindow()
    {
        InitializeComponent();
        
        _viewModel = new GameViewModel();
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        
        _incomeTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        
        _incomeTimer.Tick += OnTick;
        _incomeTimer.Start();
        
        _cashDisplay = this.FindControl<TextBlock>("CashDisplay")!;
        _baseCashDisplay = this.FindControl<TextBlock>("BaseCashDisplay")!;
        _multiplierDisplay = this.FindControl<TextBlock>("MultiplierDisplay")!;
        _incomeDisplay = this.FindControl<TextBlock>("IncomeDisplay")!;
        _plus1CostDisplay = this.FindControl<TextBlock>("Plus1CostDisplay")!;
        _x2CostDisplay = this.FindControl<TextBlock>("X2CostDisplay")!;
        _btnPlus1 = this.FindControl<Button>("BtnPlus1")!;
        _btnX2 = this.FindControl<Button>("BtnX2")!;
        
        UpdateUi();
    }

    private void OnTick(object? sender, EventArgs e)
    {
        _viewModel.GainCash();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnBuyPlus1(object? sender, RoutedEventArgs e)
    {
        _viewModel.BuyPlus1();
    }

    private void OnBuyX2(object? sender, RoutedEventArgs e)
    {
        _viewModel.BuyX2();
    }

    private void OnExitGame(object? sender, RoutedEventArgs e)
    {
        _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        _incomeTimer.Tick -= OnTick;
        
        _incomeTimer.Stop();
        
        Close();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        UpdateUi();
    }

    private void UpdateUi()
    {
        _cashDisplay.Text = _viewModel.CashDisplay;
        _baseCashDisplay.Text = _viewModel.BaseCashDisplay;
        _multiplierDisplay.Text = _viewModel.MultiplierDisplay;
        _incomeDisplay.Text = _viewModel.IncomeDisplay;
        _plus1CostDisplay.Text = _viewModel.Plus1CostDisplay;
        _x2CostDisplay.Text = _viewModel.X2CostDisplay;
        
        _btnPlus1.IsEnabled = _viewModel.CanAffordPlus1;
        _btnX2.IsEnabled = _viewModel.CanAffordX2;
        
        _plus1CostDisplay.Foreground = _viewModel.CanAffordPlus1 ? AffordableColor : UnaffordableColor;
        _x2CostDisplay.Foreground = _viewModel.CanAffordX2 ? AffordableColor : UnaffordableColor;
    }
}



