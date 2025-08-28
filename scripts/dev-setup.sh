#!/usr/bin/env bash
set -euo pipefail

# Ensure dotnet is installed (local to repo if needed)
if ! command -v dotnet >/dev/null 2>&1; then
  echo "Installing .NET SDK locally..."
  mkdir -p "$(pwd)/.dotnet" "$(pwd)/.dotnet/tools"
  curl -sSL https://dot.net/v1/dotnet-install.sh -o "$(pwd)/dotnet-install.sh"
  chmod +x "$(pwd)/dotnet-install.sh"
  "$(pwd)/dotnet-install.sh" --channel 8.0 --install-dir "$(pwd)/.dotnet"
  export PATH="$(pwd)/.dotnet:$(pwd)/.dotnet/tools:$PATH"
fi

echo "Restoring packages..."
export PATH="$(pwd)/.dotnet:$(pwd)/.dotnet/tools:$PATH"
dotnet restore "Daily-Use App/Daily-Use App.csproj"

echo "Done. Use scripts/run.sh to launch the app."

