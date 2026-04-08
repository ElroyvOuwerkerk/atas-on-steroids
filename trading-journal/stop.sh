#!/bin/bash
# Stop trading journal servers

DIR="$(cd "$(dirname "$0")" && pwd)"

for name in backend dashboard; do
    pid_file="$DIR/.${name}.pid"
    if [ -f "$pid_file" ]; then
        pid=$(cat "$pid_file")
        if kill -0 "$pid" 2>/dev/null; then
            kill "$pid"
            echo "Stopped $name (PID $pid)"
        fi
        rm -f "$pid_file"
    else
        echo "$name not running"
    fi
done
