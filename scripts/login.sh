#!/usr/bin/env bash
set -euo pipefail

# Usage: scripts/login.sh <base_url> <username> <password> [location]
# Example: scripts/login.sh http://localhost:5127 user1 pass1 "Delhi"

BASE_URL=${1:-http://localhost:5127}
USERNAME=${2:?username required}
PASSWORD=${3:?password required}
LOCATION=${4:-}

COOKIES=$(mktemp)
HTML=$(mktemp)

cleanup() {
  rm -f "$COOKIES" "$HTML"
}
trap cleanup EXIT

# 1) Get login page to capture antiforgery token and cookie
curl -s -c "$COOKIES" "$BASE_URL/Auth/Login" -o "$HTML"

# Extract __RequestVerificationToken from the form
TOKEN=$(grep -o 'name="__RequestVerificationToken"[^>]*value="[^"]*"' "$HTML" | sed -E 's/.*value="([^"]*)".*/\1/')
if [[ -z "$TOKEN" ]]; then
  echo "Failed to capture antiforgery token from $BASE_URL/Auth/Login" >&2
  exit 1
fi

# 2) Post login with credentials and optional location
curl -s -b "$COOKIES" -c "$COOKIES" \
  -X POST "$BASE_URL/Auth/Login" \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  --data-urlencode "__RequestVerificationToken=$TOKEN" \
  --data-urlencode "username=$USERNAME" \
  --data-urlencode "password=$PASSWORD" \
  --data-urlencode "location=$LOCATION" \
  -o /dev/null -w "%{http_code}\n"

# 3) Fetch Utilities page to verify location-filtered feed
echo "--- Utilities (location-filtered) ---"
curl -s -b "$COOKIES" "$BASE_URL/UtilityStatus" | sed -n '1,200p'

