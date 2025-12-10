// Copyright (c) 2025 Nico. All Rights Reserved.
// This file is part of Incremental Game and is proprietary software.
// Unauthorized copying, modification, or distribution is strictly prohibited.

using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Input;
using Avalonia.VisualTree;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity; // for RoutedEventArgs
using Avalonia.Threading; // for DispatcherTimer
using Incremental;

namespace UI;

public class StackDisplay : UserControl
{
    // Game logic
    private readonly GameLogic _gameLogic;

    // UI-Referenzen
    private TextBox _cashBox = null!;
    private Button _btnX2 = null!;
    private Button _btnPlus1 = null!;
    private TextBlock _x2CostText = null!;
    private TextBlock _plus1CostText = null!;

    // Timer für passives Einkommen
    private readonly DispatcherTimer _incomeTimer;


    public StackDisplay()
    {
        // Initialize game logic
        _gameLogic = new GameLogic();
        _gameLogic.CashChanged += (_, _) => UpdateUI();
        _gameLogic.UpgradeCostsChanged += (_, _) => UpdateCosts();

        // Timer für passives Einkommen (1 Geld pro Sekunde)
        _incomeTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _incomeTimer.Tick += (_, _) => _gameLogic.GainCash();
        _incomeTimer.Start();

        // Hauptcontainer
        var root = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            Spacing = 8,
        };

        // Header
        var header = new Border
        {
            Background = Brushes.Black,
            BorderThickness = new Thickness(0, 0, 0, 1),
            BorderBrush = Brushes.Black,
            Child = new TextBlock
            {
                Text = "Incremental UI",
                FontWeight = FontWeight.Bold,
                FontFamily = new FontFamily("Round"),
                FontSize = 33,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(6),
                Foreground = Brushes.White
            }
        };
        root.Children.Add(header);

        // Cash-Zeile: "Cash:" + Eingabefeld
        var cashRow = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Spacing = 8,
            Margin = new Thickness(0, 8, 0, 8),
        };

        var cashLabel = new TextBlock
        {
            Text = "Cash:",
            FontWeight = FontWeight.Bold,
            FontFamily = new FontFamily("Round"),
            FontSize = 22,
            Foreground = Brushes.LawnGreen,
            VerticalAlignment = VerticalAlignment.Center
        };

        _cashBox = new TextBox
        {
            Width = 180,
            Height = 34,
            FontFamily = new FontFamily("Round"),
            FontSize = 18,
            HorizontalContentAlignment = HorizontalAlignment.Right,
            VerticalContentAlignment = VerticalAlignment.Center,
            IsReadOnly = true, // Anzeige-Feld
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(1),
            Background = Brushes.White,
            Foreground = Brushes.Black,
            Text = GameLogic.FormatCash(_gameLogic.Cash)
        };

        cashRow.Children.Add(cashLabel);
        cashRow.Children.Add(_cashBox);
        root.Children.Add(cashRow);

        // Mittleres Raster mit Seitenabständen und zentriertem Upgrades-Panel
        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("1*,3*,1*"),
            RowDefinitions = new RowDefinitions("Auto"),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        // Upgrades-Panel (zentriert, groß, mit Rand und Hintergrund)
        var upgradesBorder = new Border
        {
            CornerRadius = new CornerRadius(16),
            BorderBrush = Brushes.White,
            BorderThickness = new Thickness(2),
            Background = new SolidColorBrush(Color.Parse("#2B2B2B")), // dunkles Grau
            Padding = new Thickness(16),
            Margin = new Thickness(0, 0, 0, 16)
        };
        Grid.SetColumn(upgradesBorder, 1);

        var upgradesStack = new StackPanel
        {
            Spacing = 12
        };

        // Titel "Upgrades"
        var upgradesTitle = new TextBlock
        {
            Text = "Upgrades",
            FontFamily = new FontFamily("Round"),
            FontWeight = FontWeight.Bold,
            FontSize = 26,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            Effect = new DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 1,
                BlurRadius = 0,
                OffsetX = 1,
                OffsetY = 1
            }
        };
        upgradesStack.Children.Add(upgradesTitle);

        // Scrollbarer Bereich für viele zukünftige Upgrades
        var scroll = new ScrollViewer
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            Height = 380 // genug Höhe, damit fast die ganze Fläche genutzt wird
        };

        // Inneres Grid zentriert, groß, mit Platz an den Seiten
        var innerGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("Auto,16,Auto"),
            RowDefinitions = new RowDefinitions("Auto"),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top
        };

        // Upgrade-Karten
        _btnX2 = CreateUpgradeCard(
            title: "2× Cash",
            desc: "Verdoppelt deinen Cash-Multiplikator.",
            costBinding: out _x2CostText,
            onClick: TryBuyX2);

        _btnPlus1 = CreateUpgradeCard(
            title: "+1 Base Cash",
            desc: "Erhöht Basis-Cash pro Tick/Klick um 1.",
            costBinding: out _plus1CostText,
            onClick: TryBuyPlus1);

        // Layout: zwei Karten nebeneinander
        innerGrid.Children.Add(_btnX2);
        var spacer = new Control { Width = 16 };
        Grid.SetColumn(spacer, 1);
        innerGrid.Children.Add(spacer);
        Grid.SetColumn(_btnPlus1, 2);
        innerGrid.Children.Add(_btnPlus1);

        scroll.Content = innerGrid;
        upgradesStack.Children.Add(scroll);
        upgradesBorder.Child = upgradesStack;
        grid.Children.Add(upgradesBorder);

        root.Children.Add(grid);

        // Initiale Kostenanzeige
        UpdateCosts();
        Content = root;

        // Beispiel: Cash passiv zum Test erhöhen, Taste „C“ simuliert Einnahme
        this.AttachedToVisualTree += (_, _) =>
        {
            var topLevel = this.GetVisualRoot() as TopLevel;
            if (topLevel != null)
            {
                topLevel.KeyDown += (_, e) =>
                {
                    if (e.Key == Key.C)
                    {
                        _gameLogic.GainCash();
                        e.Handled = true;
                    }
                };
            }
        };
    }

    private void TryBuyX2(object? sender, RoutedEventArgs e)
    {
        _gameLogic.TryBuyX2();
    }

    private void TryBuyPlus1(object? sender, RoutedEventArgs e)
    {
        _gameLogic.TryBuyPlus1();
    }

    private void UpdateUI()
    {
        _cashBox.Text = GameLogic.FormatCash(_gameLogic.Cash);
        UpdateCosts();
    }

    private void UpdateCosts()
    {
        _x2CostText.Text = $"Kosten: {GameLogic.FormatCash(_gameLogic.X2Cost)}";
        _plus1CostText.Text = $"Kosten: {GameLogic.FormatCash(_gameLogic.Plus1Cost)}";
    }

    private Button CreateUpgradeCard(string title, string desc, out TextBlock costBinding,
        EventHandler<RoutedEventArgs> onClick)
    {
        var card = new Border
        {
            Background = new SolidColorBrush(Color.Parse("#D9D9D9")), // helles Grau
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(12),
            BorderBrush = Brushes.Black,
            BorderThickness = new Thickness(1),
            Width = 280,
            Height = 160
        };

        var stack = new StackPanel { Spacing = 6 };

        var titleBlock = new TextBlock
        {
            Text = title,
            FontFamily = new FontFamily("Round"),
            FontWeight = FontWeight.Bold,
            FontSize = 22,
            Foreground = Brushes.White,
            Effect = new DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 1,
                BlurRadius = 0,
                OffsetX = 1,
                OffsetY = 1
            }
        };

        var descBlock = new TextBlock
        {
            Text = desc,
            FontFamily = new FontFamily("Round"),
            FontSize = 14,
            Foreground = Brushes.White,
            TextWrapping = TextWrapping.Wrap,
            Effect = new DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 1,
                BlurRadius = 0,
                OffsetX = 1,
                OffsetY = 1
            }
        };

        costBinding = new TextBlock
        {
            Text = "Cost: 0",
            FontFamily = new FontFamily("Round"),
            FontSize = 16,
            FontWeight = FontWeight.SemiBold,
            Foreground = Brushes.White,
            Effect = new DropShadowEffect
            {
                Color = Colors.Black,
                Opacity = 1,
                BlurRadius = 0,
                OffsetX = 1,
                OffsetY = 1
            }
        };

        var btn = new Button
        {
            Content = "Kaufen",
            FontFamily = new FontFamily("Round"),
            FontWeight = FontWeight.Bold,
            FontSize = 16,
            Height = 38,
            HorizontalAlignment = HorizontalAlignment.Stretch
        };
        btn.Click += onClick;

        stack.Children.Add(titleBlock);
        stack.Children.Add(descBlock);
        stack.Children.Add(costBinding);
        stack.Children.Add(btn);

        card.Child = stack;

        var container = new Button
        {
            Content = card,
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Padding = new Thickness(0)
        };
        // Outer Button als gesamter klickbarer Bereich
        container.Click += onClick;

        return container;
    }
}