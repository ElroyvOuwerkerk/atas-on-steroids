import { useStats } from "../api/stats";
import { useTrades } from "../api/trades";
import { signalColor } from "../utils/colors";
import { formatPnl } from "../utils/format";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  LineChart,
  Line,
  CartesianGrid,
  Cell,
} from "recharts";

function SummaryCard({
  label,
  value,
  color,
}: {
  label: string;
  value: string;
  color?: string;
}) {
  return (
    <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
      <div className="text-xs text-zinc-500 mb-1">{label}</div>
      <div className={`text-lg font-semibold font-mono ${color ?? "text-zinc-100"}`}>
        {value}
      </div>
    </div>
  );
}

export default function Stats() {
  const { data: stats, isLoading: statsLoading } = useStats();
  const { data: trades } = useTrades();

  if (statsLoading || !stats) {
    return <div className="text-zinc-500 py-8 text-center">Loading...</div>;
  }

  const { overall, by_signal } = stats;

  // Cumulative P&L line data
  const cumulative: { date: string; pnl: number }[] = [];
  if (trades) {
    const sorted = [...trades].sort(
      (a, b) => new Date(a.exit_time).getTime() - new Date(b.exit_time).getTime()
    );
    let running = 0;
    for (const t of sorted) {
      running += t.pnl;
      cumulative.push({
        date: new Date(t.exit_time).toLocaleDateString(),
        pnl: Math.round(running * 100) / 100,
      });
    }
  }

  // Win rate bar data
  const winRateData = by_signal.map((s) => ({
    signal: s.signal.replace(/_/g, " "),
    raw_signal: s.signal,
    win_rate: s.win_rate,
  }));

  // Avg P&L bar data
  const avgPnlData = by_signal.map((s) => ({
    signal: s.signal.replace(/_/g, " "),
    raw_signal: s.signal,
    avg_pnl: s.avg_pnl,
  }));

  return (
    <div className="space-y-6">
      <h1 className="text-xl font-semibold text-zinc-100">Performance Stats</h1>

      {/* Summary cards */}
      <div className="grid grid-cols-2 sm:grid-cols-4 gap-3">
        <SummaryCard label="Total Trades" value={String(overall.total)} />
        <SummaryCard
          label="Win Rate"
          value={`${overall.win_rate}%`}
          color={overall.win_rate >= 50 ? "text-green-400" : "text-red-400"}
        />
        <SummaryCard
          label="Total P&L"
          value={formatPnl(overall.cumulative_pnl)}
          color={overall.cumulative_pnl >= 0 ? "text-green-400" : "text-red-400"}
        />
        <SummaryCard label="Avg P&L" value={formatPnl(overall.avg_pnl)} />
      </div>

      {/* Charts */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Win rate per signal */}
        <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
          <h2 className="text-sm text-zinc-400 mb-3">Win Rate by Signal</h2>
          <ResponsiveContainer width="100%" height={250}>
            <BarChart data={winRateData}>
              <XAxis
                dataKey="signal"
                tick={{ fill: "#a1a1aa", fontSize: 11 }}
                angle={-25}
                textAnchor="end"
                height={60}
              />
              <YAxis
                tick={{ fill: "#a1a1aa", fontSize: 11 }}
                domain={[0, 100]}
                unit="%"
              />
              <Tooltip
                contentStyle={{
                  background: "#1a1d27",
                  border: "1px solid #3f3f46",
                  borderRadius: 8,
                  color: "#e4e4e7",
                }}
                formatter={(v) => [`${v}%`, "Win Rate"]}
              />
              <Bar dataKey="win_rate" radius={[4, 4, 0, 0]}>
                {winRateData.map((entry) => (
                  <Cell
                    key={entry.signal}
                    fill={signalColor(entry.raw_signal)}
                  />
                ))}
              </Bar>
            </BarChart>
          </ResponsiveContainer>
        </div>

        {/* Avg P&L per signal */}
        <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
          <h2 className="text-sm text-zinc-400 mb-3">Avg P&L by Signal</h2>
          <ResponsiveContainer width="100%" height={250}>
            <BarChart data={avgPnlData}>
              <XAxis
                dataKey="signal"
                tick={{ fill: "#a1a1aa", fontSize: 11 }}
                angle={-25}
                textAnchor="end"
                height={60}
              />
              <YAxis tick={{ fill: "#a1a1aa", fontSize: 11 }} />
              <Tooltip
                contentStyle={{
                  background: "#1a1d27",
                  border: "1px solid #3f3f46",
                  borderRadius: 8,
                  color: "#e4e4e7",
                }}
                formatter={(v) => [formatPnl(Number(v)), "Avg P&L"]}
              />
              <Bar dataKey="avg_pnl" radius={[4, 4, 0, 0]}>
                {avgPnlData.map((entry) => (
                  <Cell
                    key={entry.signal}
                    fill={entry.avg_pnl >= 0 ? "#22c55e" : "#ef4444"}
                  />
                ))}
              </Bar>
            </BarChart>
          </ResponsiveContainer>
        </div>
      </div>

      {/* Cumulative P&L */}
      <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
        <h2 className="text-sm text-zinc-400 mb-3">Cumulative P&L</h2>
        {cumulative.length > 0 ? (
          <ResponsiveContainer width="100%" height={300}>
            <LineChart data={cumulative}>
              <CartesianGrid strokeDasharray="3 3" stroke="#2e3140" />
              <XAxis
                dataKey="date"
                tick={{ fill: "#a1a1aa", fontSize: 11 }}
              />
              <YAxis tick={{ fill: "#a1a1aa", fontSize: 11 }} />
              <Tooltip
                contentStyle={{
                  background: "#1a1d27",
                  border: "1px solid #3f3f46",
                  borderRadius: 8,
                  color: "#e4e4e7",
                }}
                formatter={(v) => [formatPnl(Number(v)), "P&L"]}
              />
              <Line
                type="monotone"
                dataKey="pnl"
                stroke="#3b82f6"
                strokeWidth={2}
                dot={false}
              />
            </LineChart>
          </ResponsiveContainer>
        ) : (
          <div className="text-zinc-500 text-sm py-8 text-center">
            No trade data yet
          </div>
        )}
      </div>
    </div>
  );
}
