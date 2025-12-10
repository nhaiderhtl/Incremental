# Incremental Game

🚧 **Work In Progress - Under Active Development** 🚧

A modern incremental/idle game built with Avalonia UI and .NET 8, featuring a custom big number system, passive income mechanics, and a beautiful GitHub Dark-themed interface.

## 🎮 Features

### Currently Implemented ✅

- **Passive Income System**: Automatically earn money every second - no clicking required!
- **Custom BigNum System**: Advanced number formatting with suffix support (0, 1, 2... 999, 1.00 K, 999.99 K, 1.00 M, etc.)
- **Two Core Upgrades**:
  - **+1 Base Cash** (Starting cost: 10) - Increases base cash earned per second by 1
  - **2× Multiplier** (Starting cost: 100) - Doubles your cash multiplier
- **Dynamic Cost Colors**: 
  - 🟢 **Green** when you can afford an upgrade
  - 🔴 **Red** when you cannot afford it (updates in real-time!)
- **Modern XAML UI**: Clean, responsive interface with GitHub Dark theme
- **Tab Navigation**: Switch between Game and Settings pages
- **Statistics Panel**: Real-time display of:
  - Base Cash per second
  - Current multiplier
  - Total income per second
- **Settings Page**: 
  - Game information
  - Controls guide
  - Easy exit button (no Alt+F4 needed!)
- **MVVM Architecture**: Clean separation between UI and game logic

### Planned Features 🚀

- More upgrade types
- Save/Load system
- Achievements
- Prestige system
- Sound effects
- Animations
- Extended suffix support beyond current implementation

## 🏗️ Project Structure

```
Incremental/
├── BigNum/                 # Custom big number library
│   ├── BigNum.cs          # Main BigNum class with arithmetic operators
│   └── Suffix.cs          # Enum for number suffixes (K, M, B, T, Qd, Qn, etc.)
├── Incremental/           # Core game logic library
│   ├── GameLogic.cs       # Core game mechanics and state management
│   ├── GameViewModel.cs   # ViewModel for UI binding (MVVM pattern)
│   └── Money.cs           # Money management system (legacy)
├── UI/                    # Avalonia UI application
│   ├── Program.cs         # Application entry point
│   ├── MainWindow.axaml   # XAML UI definition (Game + Settings tabs)
│   ├── MainWindow.cs      # Code-behind with ViewModel integration
│   └── UI.csproj          # Project configuration
└── Incremental.Tests/     # Unit tests (25 tests, all passing)
    ├── BigNumTests.cs     # BigNum calculation tests
    └── MoneyTests.cs      # Money system tests
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Linux, macOS, or Windows

### Installation

1. Clone this repository:
   ```bash
   git clone <repository-url>
   cd Incremental
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

### Running the Game

Run the game using:
```bash
dotnet run --project UI/UI.csproj
```

The game will open in a resizable window with tab navigation (Game and Settings).

## 🎯 How to Play

### Basic Mechanics

- **Passive Income**: Cash is automatically generated every second based on your base cash and multiplier
- **Upgrades**: Click on upgrade buttons when they're enabled (green cost) to purchase them
- **Tab Navigation**: Switch between the **🎮 Game** tab and **⚙️ Settings** tab

### Understanding the Interface

#### Game Tab
- **Cash Display**: Shows your current money with automatic suffix formatting
- **Statistics Panel**: 
  - Base Cash/sec: How much base cash you earn per second
  - Multiplier: Your current cash multiplier
  - Total Income/sec: Base × Multiplier (your actual income rate)
- **Upgrades Panel**: 
  - Upgrade cards show in green when affordable, red when too expensive
  - Click to purchase when you have enough cash
  - Costs scale up after each purchase

#### Settings Tab
- View game information (version, creator, year)
- Read controls guide
- **Exit Game** button - Cleanly close the application

### Upgrade Strategy

1. Start by saving up for the **+1 Base Cash** upgrade (10 cash) to increase your passive income rate
2. Once you have steady income, purchase **2× Multiplier** (100 cash) to multiply your earnings
3. Balance between both upgrades for exponential growth
4. Watch the cost colors: Green = affordable, Red = keep saving!

## 🧪 Testing

Run the test suite:
```bash
dotnet test
```

All tests should pass:
- **BigNum Tests**: Validates number parsing, formatting, and arithmetic operations
- **Money Tests**: Ensures proper money calculations

## 🛠️ Development

### Building Individual Projects

```bash
# Build BigNum library
dotnet build BigNum/BigNum.csproj

# Build UI
dotnet build UI/UI.csproj

# Build tests
dotnet build Incremental.Tests/Incremental.Tests.csproj
```

### Running Tests with Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 📊 BigNum System

The custom BigNum system supports arithmetic with large numbers using suffixes:

- **None**: Base numbers (1-999)
- **K**: Thousands (1,000+)
- **M**: Millions (1,000,000+)
- **B**: Billions (1,000,000,000+)
- **T**: Trillions
- **Qd**: Quadrillions
- **Qn**: Quintillions
- **Sx**: Sextillions
- **Sp**: Septillions
- **Oc**: Octillions
- **No**: Nonillions
- **De**: Decillions
- And many more...

### Example Operations

```csharp
var a = new BigNum(600, Suffix.K);  // 600K
var b = new BigNum(500, Suffix.K);  // 500K
var result = a + b;                 // 1.1M

var c = new BigNum(0.5, Suffix.M);  // 0.5M
var d = new BigNum(0.2, Suffix.M);  // 0.2M
var (diff, success) = c - d;        // 300K
```

## 🔧 Technical Details

- **Framework**: .NET 8.0
- **UI Framework**: Avalonia 11.x (XAML-based)
- **Language**: C# 12
- **Architecture**: MVVM-inspired pattern with ViewModel layer
- **Design Theme**: GitHub Dark color scheme
- **Passive Income**: DispatcherTimer with 1-second intervals
- **Testing**: xUnit with 25 unit tests (all passing)
- **Number System**: Custom BigNum implementation with suffix support

## 📝 License & Copyright

**Copyright © 2025 Nico. All Rights Reserved.**

This project and all of its contents are the exclusive property of Nico.

### Terms of Use

- ❌ **NO COPYING**: This code may not be copied, reproduced, or distributed in any form
- ❌ **NO MODIFICATION**: Creating derivative works is strictly prohibited
- ❌ **NO COMMERCIAL USE**: This software may not be used for commercial purposes
- ❌ **NO REDISTRIBUTION**: Sharing or publishing this code is not permitted
- ⚠️ **VIEWING ONLY**: This repository is available for viewing and personal learning only

**All rights, title, and interest in and to this software remain with Nico.**

Any unauthorized use, copying, modification, or distribution of this software is strictly prohibited and may result in severe civil and criminal penalties.

## 👤 Author

**Nico**

This project was created and developed entirely by Nico in December 2025.

## 🤝 Contributing

This is a personal project and is **not accepting contributions**.
If you wish to share feedback or ideas, please contact me. I'd appreciate it.

---

**Made with ❤️ by Nico | December 2025**

**All credits and ownership belong exclusively to Nico - Forever and Always**

**All credits and ownership belong exclusively to Nico - Forever and Always**

