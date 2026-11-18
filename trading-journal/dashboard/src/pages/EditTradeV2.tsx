import { useParams, useNavigate, Link } from "react-router-dom";
import { useV2Trade, useUpdateV2Trade, useDeleteV2Trade } from "../api/tradesV2";
import TradeForm from "../components/TradeForm";
import { formatPnl } from "../utils/format";

function Metric({ label, value, color }: { label: string; value: string; color?: string }) {
  return (
    <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg px-3 py-2">
      <div className="text-xs text-zinc-500">{label}</div>
      <div className={`text-sm font-mono font-semibold ${color ?? "text-zinc-100"}`}>{value}</div>
    </div>
  );
}

export default function EditTradeV2() {
  const { id } = useParams<{ id: string }>();
  const tradeId = Number(id);
  const nav = useNavigate();
  const { data: trade, isLoading, error } = useV2Trade(tradeId);
  const update = useUpdateV2Trade(tradeId);
  const del = useDeleteV2Trade();

  if (isLoading) return <div className="text-zinc-500 py-8 text-center">Loading...</div>;
  if (error || !trade) return <div className="text-red-400 py-8 text-center">Trade not found.</div>;

  const r = trade.realized_r;
  const pnl = trade.computed_pnl ?? trade.pnl;

  return (
    <div className="space-y-5">
      <Link to="/journal" className="text-sm text-zinc-400 hover:text-zinc-200 inline-block">
        &larr; Back to journal
      </Link>

      <div className="flex items-center gap-3 flex-wrap">
        <h1 className="text-xl font-semibold text-zinc-100">
          {trade.instrument} <span className={trade.direction === "Long" ? "text-green-400" : "text-red-400"}>{trade.direction}</span>
        </h1>
        {trade.grade && (
          <span className="text-xs px-2 py-0.5 rounded bg-zinc-700 text-zinc-200">Grade {trade.grade}</span>
        )}
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-5 gap-2">
        <Metric
          label="Realized R"
          value={r === null ? "no stop" : `${r >= 0 ? "+" : ""}${r.toFixed(2)}R`}
          color={r === null ? "text-zinc-500" : r >= 0 ? "text-green-400" : "text-red-400"}
        />
        <Metric label="P&L" value={formatPnl(pnl)} color={pnl >= 0 ? "text-green-400" : "text-red-400"} />
        <Metric label="Risk %" value={trade.risk_pct === null ? "-" : `${trade.risk_pct}%`} />
        <Metric label="Scaled out" value={trade.scaled_out ? `yes (${trade.exits.length})` : "no"} />
        <Metric label="Session" value={trade.session ?? "-"} />
      </div>

      <TradeForm
        initial={trade}
        showReview
        submitLabel="Save changes"
        submitting={update.isPending}
        onSubmit={(payload) => update.mutate(payload, { onSuccess: () => nav("/journal") })}
      />

      {update.isError && <div className="text-sm text-red-400">Failed to save.</div>}

      <div className="pt-4 border-t border-zinc-800">
        <button
          onClick={() => {
            if (window.confirm("Delete this trade? This cannot be undone.")) {
              del.mutate(tradeId, { onSuccess: () => nav("/journal") });
            }
          }}
          className="text-xs text-red-400 hover:text-red-300"
        >
          Delete trade
        </button>
      </div>
    </div>
  );
}
