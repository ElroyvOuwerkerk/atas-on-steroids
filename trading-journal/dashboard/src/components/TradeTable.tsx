import { Link } from "react-router-dom";
import type { Trade } from "../types/trade";
import { formatDateTime, formatPnl } from "../utils/format";
import SignalBadge from "./SignalBadge";

export default function TradeTable({ trades }: { trades: Trade[] }) {
  if (trades.length === 0) {
    return (
      <div className="text-center py-16 text-zinc-500">
        No trades yet. Trades will appear here once the ATAS plugin sends data.
      </div>
    );
  }

  return (
    <div className="overflow-x-auto">
      <table className="w-full text-sm">
        <thead>
          <tr className="border-b border-zinc-700 text-zinc-400 text-left">
            <th className="py-2 px-3 font-medium">Date</th>
            <th className="py-2 px-3 font-medium">Instrument</th>
            <th className="py-2 px-3 font-medium">Direction</th>
            <th className="py-2 px-3 font-medium">Signal</th>
            <th className="py-2 px-3 font-medium text-right">P&L</th>
            <th className="py-2 px-3 font-medium text-center">Result</th>
          </tr>
        </thead>
        <tbody>
          {trades.map((t) => (
            <Link
              key={t.id}
              to={`/trades/${t.id}`}
              className="contents"
            >
              <tr className="border-b border-zinc-800 hover:bg-zinc-800/50 cursor-pointer transition-colors">
                <td className="py-2.5 px-3 text-zinc-300">
                  {formatDateTime(t.exit_time)}
                </td>
                <td className="py-2.5 px-3 font-mono text-zinc-200">
                  {t.instrument}
                </td>
                <td className="py-2.5 px-3">
                  <span
                    className={
                      t.direction === "Long" ? "text-green-400" : "text-red-400"
                    }
                  >
                    {t.direction}
                  </span>
                </td>
                <td className="py-2.5 px-3">
                  <SignalBadge signal={t.signal} />
                </td>
                <td
                  className={`py-2.5 px-3 text-right font-mono ${
                    t.pnl >= 0 ? "text-green-400" : "text-red-400"
                  }`}
                >
                  {formatPnl(t.pnl)}
                </td>
                <td className="py-2.5 px-3 text-center">
                  {t.pnl >= 0 ? (
                    <span className="text-green-400">W</span>
                  ) : (
                    <span className="text-red-400">L</span>
                  )}
                </td>
              </tr>
            </Link>
          ))}
        </tbody>
      </table>
    </div>
  );
}
