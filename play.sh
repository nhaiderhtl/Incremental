#!/bin/bash
# Incremental Game Launcher
# Copyright (c) 2025 Nico. All Rights Reserved.

set -e  # Exit on any error

echo "======================================"
echo "  Incremental Game Launcher"
echo "======================================"
echo ""

# Get the directory where the script is located
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

# Check if .NET 8 SDK is installed
echo "[1/4] Checking for .NET 8 SDK..."
if ! command -v dotnet &> /dev/null; then
    echo "ERROR: .NET 8 SDK is not installed!"
    echo "Please install it from: https://dotnet.microsoft.com/download/dotnet/8.0"
    exit 1
fi

# Verify .NET version
DOTNET_VERSION=$(dotnet --version)
echo "Found .NET version: $DOTNET_VERSION"
echo ""

# Restore dependencies
echo "[2/4] Restoring dependencies..."
dotnet restore --verbosity quiet
if [ $? -eq 0 ]; then
    echo "Dependencies restored successfully."
else
    echo "ERROR: Failed to restore dependencies."
    exit 1
fi
echo ""

# Build the project
echo "[3/4] Building the project..."
dotnet build --no-restore --verbosity quiet
if [ $? -eq 0 ]; then
    echo "Build completed successfully."
else
    echo "ERROR: Build failed."
    exit 1
fi
echo ""

# Run the game
echo "[4/4] Starting Incremental Game..."
echo "======================================"
echo ""
cd UI
dotnet run --project UI.csproj

# Exit message (only shown if game is closed normally)
echo ""
echo "======================================"
echo "  Game closed. Thanks for playing!"
echo "======================================"

