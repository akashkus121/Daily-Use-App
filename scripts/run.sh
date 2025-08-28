#!/usr/bin/env bash
set -euo pipefail

export PATH="$(pwd)/.dotnet:$(pwd)/.dotnet/tools:$PATH"

dotnet build "Daily-Use App/Daily-Use App.csproj"

ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT:-Development} \
dotnet run --project "Daily-Use App/Daily-Use App.csproj"

