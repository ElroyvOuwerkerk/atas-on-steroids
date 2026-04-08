#!/bin/bash
# Start trading journal servers and launch ATAS X
# Usage: ./start.sh        (start servers + open ATAS)
#        ./start.sh --no-atas  (start servers only)

DIR="$(cd "$(dirname "$0")" && pwd)"
BACKEND_PID_FILE="$DIR/.backend.pid"
DASHBOARD_PID_FILE="$DIR/.dashboard.pid"

is_running() {
    local pid_file="$1"
    if [ -f "$pid_file" ]; then
        local pid
        pid=$(cat "$pid_file")
        if kill -0 "$pid" 2>/dev/null; then
            return 0
        fi
        rm -f "$pid_file"
    fi
    return 1
}

# Start backend
if is_running "$BACKEND_PID_FILE"; then
    echo "Backend already running (PID $(cat "$BACKEND_PID_FILE"))"
else
    echo "Starting backend..."
    cd "$DIR/backend"
    python3 -m uvicorn app.main:app --host 0.0.0.0 --port 3001 &
    echo $! > "$BACKEND_PID_FILE"
    echo "Backend started (PID $!)"
    cd "$DIR"
fi

# Start dashboard
if is_running "$DASHBOARD_PID_FILE"; then
    echo "Dashboard already running (PID $(cat "$DASHBOARD_PID_FILE"))"
else
    echo "Starting dashboard..."
    cd "$DIR/dashboard"
    export PATH="/opt/homebrew/bin:$PATH"
    npx vite --port 5173 &
    echo $! > "$DASHBOARD_PID_FILE"
    echo "Dashboard started (PID $!) → http://localhost:5173"
    cd "$DIR"
fi

# Launch ATAS X unless --no-atas
if [ "$1" != "--no-atas" ]; then
    if ! pgrep -x "ATAS X" >/dev/null 2>&1; then
        echo "Opening ATAS X..."
        open -a "ATAS X"
    else
        echo "ATAS X already running"
    fi
fi

echo "Done. Servers running."
