import { Link } from "react-router-dom";
import { useV2Trades } from "../api/tradesV2";
import { formatDateTime, formatPnl } from "../utils/format";

function rLabel(r: number | null): string {
  if (r === null) return "-";
  return `${r >= 0 ? "+" : ""}${r.toFixed(2)}R`;
}

export default function JournalV2() {
  const { data: trades, isLoading, error } = useV2Trades();

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-xl font-semibold text-zinc-100">Journal</h1>
        <Link
          to="/journal/new"
          className="bg-zinc-200 text-zinc-900 hover:bg-white font-medium rounded-lg px-4 py-1.5 text-sm"
        >
          + New trade
        </Link>
      </div>

      {isLoading && <div className="text-zinc-500 py-8 text-center">Loading...</div>}
      {error && (
        <div className="text-red-400 py-8 text-center">
          Failed to load. Is the backend running on port 3001?
        </div>
      )}

      {trades && trades.length === 0 && (
        <div className="text-center py-16 text-zinc-500">
          No trades yet. Add one with partial exits to start measuring R.
        </div>
      )}

      {trades && trades.length > 0 && (
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-zinc-700 text-zinc-400 text-left">
                <th className="py-2 px-3 font-medium">Date</th>
                <th className="py-2 px-3 font-medium">Instrument</th>
                <th className="py-2 px-3 font-medium">Dir</th>
                <th className="py-2 px-3 font-medium">Strategy</th>
                <th className="py-2 px-3 font-medium text-center">Exits</th>
                <th className="py-2 px-3 font-medium text-right">R</th>
                <th className="py-2 px-3 font-medium text-right">P&L</th>
              </tr>
            </thead>
            <tbody>
              {trades.map((t) => {
                const pnl = t.computed_pnl ?? t.pnl;
                return (
                  <Link key={t.id} to={`/journal/${t.id}`} className="contents">
                    <tr className="border-b border-zinc-800 hover:bg-zinc-800/50 cursor-pointer transition-colors">
                      <td className="py-2.5 px-3 text-zinc-300">{formatDateTime(t.exit_time)}</td>
                      <td className="py-2.5 px-3 font-mono text-zinc-200">{t.instrument}</td>
                      <td className="py-2.5 px-3">
                        <span className={t.direction === "Long" ? "text-green-400" : "text-red-400"}>
                          {t.direction}
                        </span>
                      </td>
                      <td className="py-2.5 px-3 text-zinc-300">{t.strategy ?? t.signal ?? "-"}</td>
                      <td className="py-2.5 px-3 text-center text-zinc-400">
                        {t.scaled_out ? t.exits.length : t.exits.length || 1}
                      </td>
                      <td
                        className={`py-2.5 px-3 text-right font-mono ${
                          t.realized_r === null
                            ? "text-zinc-500"
                            : t.realized_r >= 0
                            ? "text-green-400"
                            : "text-red-400"
                        }`}
                      >
                        {rLabel(t.realized_r)}
                      </td>
                      <td className={`py-2.5 px-3 text-right font-mono ${pnl >= 0 ? "text-green-400" : "text-red-400"}`}>
                        {formatPnl(pnl)}
                      </td>
                    </tr>
                  </Link>
                );
              })}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
