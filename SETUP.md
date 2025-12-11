# Setup and Play Guide

This guide will help you get the Incremental Game running on your system.

## Prerequisites

Before running the game, ensure you have the following installed:

- **.NET 8.0 SDK** or later
  - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
  - Verify installation: `dotnet --version`

- **Supported Operating Systems**:
  - Linux (tested on Ubuntu, Debian, Fedora)
  - macOS (10.13 or later)
  - Windows (10 or later)

## Quick Start (Recommended)

The easiest way to run the game is using the provided shell script:

### On Linux/macOS:

```bash
./play.sh
```

That's it! The script will:
1. Check for .NET 8 SDK
2. Restore all dependencies
3. Build the project
4. Launch the game

### First Time Setup

If you encounter a "permission denied" error, make the script executable:

```bash
chmod +x play.sh
./play.sh
```

## Manual Setup (Alternative)

If you prefer to run commands manually or the script doesn't work:

### Step 1: Restore Dependencies

```bash
dotnet restore
```

This downloads all required NuGet packages.

### Step 2: Build the Project

```bash
dotnet build
```

This compiles all projects in the solution.

### Step 3: Run the Game

```bash
dotnet run --project UI/UI.csproj
```

The game window will open.

## Verifying Installation

To verify everything is set up correctly, run the tests:

```bash
dotnet test
```

You should see:
```
Passed!  - Failed:     0, Passed:    25, Skipped:     0, Total:    25
```

## Troubleshooting

### "dotnet: command not found"

**Problem**: .NET SDK is not installed or not in PATH.

**Solution**:
1. Download and install .NET 8.0 SDK from https://dotnet.microsoft.com/download
2. Restart your terminal
3. Verify: `dotnet --version`

### Build Errors

**Problem**: Build fails with compilation errors.

**Solution**:
1. Clean the project: `dotnet clean`
2. Restore dependencies: `dotnet restore`
3. Rebuild: `dotnet build`

### Missing Dependencies

**Problem**: NuGet restore fails.

**Solution**:
1. Check internet connection
2. Clear NuGet cache: `dotnet nuget locals all --clear`
3. Restore again: `dotnet restore`

### Script Permission Denied (Linux/macOS)

**Problem**: `./play.sh` shows "Permission denied"

**Solution**:
```bash
chmod +x play.sh
```

### Game Won't Start

**Problem**: Build succeeds but game doesn't open.

**Solution**:
1. Check if you have a display server running (for Linux)
2. Try running with more verbosity: `dotnet run --project UI/UI.csproj --verbosity normal`
3. Check the error messages

## Development Workflow

### Building Specific Projects

```bash
# Build only the BigNum library
dotnet build BigNum/BigNum.csproj

# Build only the game logic
dotnet build Incremental/Incremental.csproj

# Build only the UI
dotnet build UI/UI.csproj

# Build only tests
dotnet build Incremental.Tests/Incremental.Tests.csproj
```

### Running Tests with Details

```bash
# Run all tests with detailed output
dotnet test --verbosity normal

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test Incremental.Tests/Incremental.Tests.csproj
```

### Clean Build

If you want to start fresh:

```bash
# Clean all build artifacts
dotnet clean

# Restore dependencies
dotnet restore

# Build everything
dotnet build

# Run tests
dotnet test

# Run game
dotnet run --project UI/UI.csproj
```

## Project Structure Overview

```
Incremental/
├── play.sh                 # Launch script (use this!)
├── BigNum/                 # Number formatting library
├── Incremental/            # Game logic
├── UI/                     # User interface
└── Incremental.Tests/      # Unit tests
```

## Running on Different Platforms

### Linux

```bash
./play.sh
```

Tested on:
- Ubuntu 22.04+
- Debian 11+
- Fedora 36+
- Arch Linux (current)

### macOS

```bash
./play.sh
```

Requirements:
- macOS 10.13 (High Sierra) or later
- .NET 8.0 SDK for macOS

### Windows

You can use the manual setup or create a batch file equivalent.

**Option 1: Manual**
```cmd
dotnet restore
dotnet build
dotnet run --project UI/UI.csproj
```

**Option 2: PowerShell**
```powershell
.\play.sh  # If you have bash installed (Git Bash, WSL)
```

## Performance Notes

- **First Run**: May take 30-60 seconds (dependencies download + compilation)
- **Subsequent Runs**: Using `play.sh` typically takes 5-10 seconds
- **Build Time**: ~3-5 seconds on modern hardware
- **Runtime Performance**: 60 FPS UI, negligible CPU usage

## Getting Help

If you encounter issues:

1. Check this guide's Troubleshooting section
2. Verify .NET SDK version: `dotnet --version` (should be 8.0.x)
3. Try a clean build: `dotnet clean && dotnet restore && dotnet build`
4. Check for error messages in terminal output

## Quick Command Reference

| Action | Command |
|--------|---------|
| **Start Game (Easy)** | `./play.sh` |
| **Start Game (Manual)** | `dotnet run --project UI/UI.csproj` |
| **Build** | `dotnet build` |
| **Test** | `dotnet test` |
| **Clean** | `dotnet clean` |
| **Restore** | `dotnet restore` |
| **Make Script Executable** | `chmod +x play.sh` |

## Next Steps

Once the game is running:
1. Watch the passive income accumulate
2. Save up for your first upgrade (10 cash for +1 Base Cash)
3. Purchase upgrades when costs turn green
4. Balance between +1 Base Cash and 2x Multiplier for optimal growth
5. Use the Settings tab to exit the game cleanly

Enjoy the game!

