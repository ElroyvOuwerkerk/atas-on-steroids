import type { Trade } from "../types/trade";
import { formatDateTime, formatPrice, formatPnl } from "../utils/format";
import SignalBadge from "./SignalBadge";
import VideoPlayer from "./VideoPlayer";
import NotesEditor from "./NotesEditor";

function Field({ label, children }: { label: string; children: React.ReactNode }) {
  return (
    <div>
      <div className="text-xs text-zinc-500 mb-0.5">{label}</div>
      <div className="text-sm text-zinc-200">{children}</div>
    </div>
  );
}

export default function TradeDetail({ trade }: { trade: Trade }) {
  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex items-center gap-3 flex-wrap">
        <h1 className="text-xl font-semibold text-zinc-100">
          {trade.instrument}
        </h1>
        <span
          className={`text-sm font-medium ${
            trade.direction === "Long" ? "text-green-400" : "text-red-400"
          }`}
        >
          {trade.direction}
        </span>
        <SignalBadge signal={trade.signal} />
        <span
          className={`ml-auto text-lg font-mono font-semibold ${
            trade.pnl >= 0 ? "text-green-400" : "text-red-400"
          }`}
        >
          {formatPnl(trade.pnl)}
        </span>
      </div>

      {/* Info grid */}
      <div className="grid grid-cols-2 sm:grid-cols-4 gap-4 bg-zinc-800/50 border border-zinc-700 rounded-lg p-4">
        <Field label="Entry Price">{formatPrice(trade.entry_price)}</Field>
        <Field label="Exit Price">{formatPrice(trade.exit_price)}</Field>
        <Field label="Size">{trade.size}</Field>
        <Field label="Signal Source">{trade.signal_source ?? "N/A"}</Field>
        <Field label="Entry Time">{formatDateTime(trade.entry_time)}</Field>
        <Field label="Exit Time">{formatDateTime(trade.exit_time)}</Field>
      </div>

      {/* Video */}
      <div>
        <h2 className="text-sm text-zinc-400 mb-2">Replay Clip</h2>
        <VideoPlayer filename={trade.clip_filename} />
      </div>

      {/* Notes */}
      <NotesEditor tradeId={trade.id} initialNotes={trade.notes} />
    </div>
  );
}
