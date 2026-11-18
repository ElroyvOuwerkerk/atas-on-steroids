import { useReview } from "../api/review";
import { formatPnl } from "../utils/format";
import type { RBucket } from "../types/tradeV2";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  Cell,
} from "recharts";

function Card({ label, value, color }: { label: string; value: string; color?: string }) {
  return (
    <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
      <div className="text-xs text-zinc-500 mb-1">{label}</div>
      <div className={`text-lg font-semibold font-mono ${color ?? "text-zinc-100"}`}>{value}</div>
    </div>
  );
}

const tooltipStyle = {
  background: "#1a1d27",
  border: "1px solid #3f3f46",
  borderRadius: 8,
  color: "#e4e4e7",
};

function AvgRChart({ title, data }: { title: string; data: RBucket[] }) {
  const rows = data.filter((d) => d.trades > 0);
  return (
    <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
      <h2 className="text-sm text-zinc-400 mb-3">{title}</h2>
      {rows.length === 0 ? (
        <div className="text-zinc-500 text-sm py-8 text-center">No data yet</div>
      ) : (
        <ResponsiveContainer width="100%" height={240}>
          <BarChart data={rows}>
            <XAxis dataKey="key" tick={{ fill: "#a1a1aa", fontSize: 11 }} angle={-20} textAnchor="end" height={55} interval={0} />
            <YAxis tick={{ fill: "#a1a1aa", fontSize: 11 }} unit="R" />
            <Tooltip
              contentStyle={tooltipStyle}
              formatter={(v, _n, item) => {
                const p = item?.payload as RBucket;
                return [`${Number(v).toFixed(2)}R  (${p.trades} trades)`, "Avg R"];
              }}
            />
            <Bar dataKey="avg_r" radius={[4, 4, 0, 0]}>
              {rows.map((d) => (
                <Cell key={d.key} fill={d.avg_r >= 0 ? "#22c55e" : "#ef4444"} />
              ))}
            </Bar>
          </BarChart>
        </ResponsiveContainer>
      )}
    </div>
  );
}

function DistChart({ data }: { data: RBucket[] }) {
  return (
    <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
      <h2 className="text-sm text-zinc-400 mb-3">R distribution (trades)</h2>
      <ResponsiveContainer width="100%" height={240}>
        <BarChart data={data}>
          <XAxis dataKey="key" tick={{ fill: "#a1a1aa", fontSize: 11 }} interval={0} />
          <YAxis tick={{ fill: "#a1a1aa", fontSize: 11 }} allowDecimals={false} />
          <Tooltip contentStyle={tooltipStyle} formatter={(v) => [String(v), "Trades"]} />
          <Bar dataKey="trades" radius={[4, 4, 0, 0]}>
            {data.map((d) => {
              const neg = d.key.includes("-") && !d.key.includes("..");
              const below = d.key.startsWith("<") || d.key.startsWith("-");
              return <Cell key={d.key} fill={below || neg ? "#ef4444" : "#22c55e"} />;
            })}
          </Bar>
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}

function BucketTable({ title, data }: { title: string; data: RBucket[] }) {
  const rows = data.filter((d) => d.trades > 0);
  if (rows.length === 0) return null;
  return (
    <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
      <h2 className="text-sm text-zinc-400 mb-3">{title}</h2>
      <table className="w-full text-sm">
        <thead>
          <tr className="text-zinc-500 text-left border-b border-zinc-700">
            <th className="py-1.5 pr-3 font-medium">Key</th>
            <th className="py-1.5 px-3 font-medium text-right">Trades</th>
            <th className="py-1.5 px-3 font-medium text-right">Avg R</th>
            <th className="py-1.5 px-3 font-medium text-right">Win %</th>
            <th className="py-1.5 pl-3 font-medium text-right">Net P&L</th>
          </tr>
        </thead>
        <tbody>
          {rows.map((d) => (
            <tr key={d.key} className="border-b border-zinc-800">
              <td className="py-1.5 pr-3 text-zinc-200">{d.key}</td>
              <td className="py-1.5 px-3 text-right font-mono text-zinc-300">{d.trades}</td>
              <td className={`py-1.5 px-3 text-right font-mono ${d.avg_r >= 0 ? "text-green-400" : "text-red-400"}`}>
                {d.avg_r.toFixed(2)}
              </td>
              <td className="py-1.5 px-3 text-right font-mono text-zinc-300">{d.win_rate}%</td>
              <td className={`py-1.5 pl-3 text-right font-mono ${d.net_pnl >= 0 ? "text-green-400" : "text-red-400"}`}>
                {formatPnl(d.net_pnl)}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default function Review() {
  const { data, isLoading, error } = useReview();

  if (isLoading) return <div className="text-zinc-500 py-8 text-center">Loading...</div>;
  if (error || !data) return <div className="text-red-400 py-8 text-center">Failed to load review.</div>;

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-xl font-semibold text-zinc-100">Review</h1>
        <p className="text-sm text-zinc-500 mt-1">
          R is the unit. Trades without a stop have no R and sit out of the R buckets, but their P&L still counts.
        </p>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-4 gap-3">
        <Card
          label="Expectancy (avg R)"
          value={`${data.expectancy_r >= 0 ? "+" : ""}${data.expectancy_r.toFixed(2)}R`}
          color={data.expectancy_r >= 0 ? "text-green-400" : "text-red-400"}
        />
        <Card
          label="Total realized R"
          value={`${data.total_realized_r >= 0 ? "+" : ""}${data.total_realized_r.toFixed(1)}R`}
          color={data.total_realized_r >= 0 ? "text-green-400" : "text-red-400"}
        />
        <Card label="Trades with R" value={String(data.trades_with_r)} />
        <Card label="Scaled out" value={`${data.scaled_out_share}%`} />
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <AvgRChart title="Avg R by session" data={data.by_session} />
        <AvgRChart title="Avg R by weekday" data={data.by_weekday} />
        <AvgRChart title="Avg R by strategy" data={data.by_strategy} />
        <DistChart data={data.r_distribution} />
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <BucketTable title="By hour (NY)" data={data.by_hour} />
        <BucketTable title="By tag / event" data={data.by_tag} />
      </div>
    </div>
  );
}
