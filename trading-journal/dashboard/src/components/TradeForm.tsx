import { useState } from "react";
import type { TradeV2, TradeV2Create, TradeV2Update } from "../types/tradeV2";
import ExitsEditor, { emptyExit, type ExitDraft } from "./ExitsEditor";
import {
  KNOWN_INSTRUMENTS,
  KNOWN_SIGNALS,
  isoToLocalInput,
  localInputToISO,
  numOrNull,
  suggestPointValue,
} from "../utils/tradeForm";

const field = "block";
const inputCls =
  "w-full bg-zinc-800 border border-zinc-700 rounded px-3 py-1.5 text-sm text-zinc-200 focus:outline-none focus:border-zinc-500";
const labelCls = "text-xs text-zinc-500 mb-0.5 block";

function seedExits(trade?: TradeV2): ExitDraft[] {
  if (!trade || trade.exits.length === 0) return [emptyExit()];
  return trade.exits.map((e) => ({
    exit_price: String(e.exit_price),
    quantity: String(e.quantity),
    exit_time: isoToLocalInput(e.exit_time),
    fees: e.fees === null ? "" : String(e.fees),
    pnl: e.pnl === null ? "" : String(e.pnl),
    note: e.note ?? "",
  }));
}

interface Props {
  initial?: TradeV2;
  showReview?: boolean;
  submitting?: boolean;
  submitLabel: string;
  onSubmit: (payload: TradeV2Create & TradeV2Update) => void;
}

export default function TradeForm({ initial, showReview, submitting, submitLabel, onSubmit }: Props) {
  const [instrument, setInstrument] = useState(initial?.instrument ?? "");
  const [direction, setDirection] = useState(initial?.direction ?? "Long");
  const [entryPrice, setEntryPrice] = useState(initial ? String(initial.entry_price) : "");
  const [size, setSize] = useState(initial ? String(initial.size) : "");
  const [entryTime, setEntryTime] = useState(isoToLocalInput(initial?.entry_time));
  const [stopLoss, setStopLoss] = useState(initial?.stop_loss != null ? String(initial.stop_loss) : "");
  const [target, setTarget] = useState(initial?.target != null ? String(initial.target) : "");
  const [pointValue, setPointValue] = useState(initial?.point_value != null ? String(initial.point_value) : "");
  const [balance, setBalance] = useState(
    initial?.account_balance_at_entry != null ? String(initial.account_balance_at_entry) : ""
  );
  const [strategy, setStrategy] = useState(initial?.strategy ?? "");
  const [tags, setTags] = useState(initial?.tags ?? "");
  const [signal, setSignal] = useState(initial?.signal ?? "");
  const [notes, setNotes] = useState(initial?.notes ?? "");
  const [review, setReview] = useState(initial?.review ?? "");
  const [grade, setGrade] = useState(initial?.grade ?? "");
  const [exits, setExits] = useState<ExitDraft[]>(seedExits(initial));
  const [error, setError] = useState<string | null>(null);

  const onInstrumentChange = (v: string) => {
    setInstrument(v);
    if (!pointValue) {
      const pv = suggestPointValue(v);
      if (pv !== null) setPointValue(String(pv));
    }
  };

  const submit = (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (!instrument.trim() || !entryPrice || !size || !entryTime) {
      setError("Instrument, entry price, size and entry time are required.");
      return;
    }
    const cleanExits = exits
      .filter((r) => r.exit_price !== "" && r.quantity !== "")
      .map((r) => ({
        exit_price: Number(r.exit_price),
        quantity: Number(r.quantity),
        exit_time: r.exit_time ? localInputToISO(r.exit_time) : null,
        fees: numOrNull(r.fees) ?? 0,
        pnl: numOrNull(r.pnl),
        note: r.note || null,
      }));

    const payload: TradeV2Create & TradeV2Update = {
      instrument: instrument.trim(),
      direction,
      entry_price: Number(entryPrice),
      size: Number(size),
      entry_time: localInputToISO(entryTime),
      stop_loss: numOrNull(stopLoss),
      target: numOrNull(target),
      point_value: numOrNull(pointValue),
      account_balance_at_entry: numOrNull(balance),
      strategy: strategy.trim() || null,
      tags: tags.trim() || null,
      signal: signal || null,
      notes: notes.trim() || null,
      exits: cleanExits,
    };
    if (showReview) {
      payload.review = review.trim() || null;
      payload.grade = grade || null;
    }
    onSubmit(payload);
  };

  return (
    <form onSubmit={submit} className="space-y-5">
      <div className="grid grid-cols-2 sm:grid-cols-4 gap-3">
        <label className={field}>
          <span className={labelCls}>Instrument</span>
          <input
            list="instruments"
            className={inputCls}
            value={instrument}
            onChange={(e) => onInstrumentChange(e.target.value)}
          />
          <datalist id="instruments">
            {KNOWN_INSTRUMENTS.map((i) => (
              <option key={i} value={i} />
            ))}
          </datalist>
        </label>
        <label className={field}>
          <span className={labelCls}>Direction</span>
          <select className={inputCls} value={direction} onChange={(e) => setDirection(e.target.value)}>
            <option value="Long">Long</option>
            <option value="Short">Short</option>
          </select>
        </label>
        <label className={field}>
          <span className={labelCls}>Entry price</span>
          <input type="number" step="any" className={inputCls} value={entryPrice} onChange={(e) => setEntryPrice(e.target.value)} />
        </label>
        <label className={field}>
          <span className={labelCls}>Size (total qty)</span>
          <input type="number" step="any" className={inputCls} value={size} onChange={(e) => setSize(e.target.value)} />
        </label>
        <label className={field}>
          <span className={labelCls}>Entry time</span>
          <input type="datetime-local" className={inputCls} value={entryTime} onChange={(e) => setEntryTime(e.target.value)} />
        </label>
        <label className={field}>
          <span className={labelCls}>Stop loss</span>
          <input type="number" step="any" className={inputCls} value={stopLoss} onChange={(e) => setStopLoss(e.target.value)} />
        </label>
        <label className={field}>
          <span className={labelCls}>Target</span>
          <input type="number" step="any" className={inputCls} value={target} onChange={(e) => setTarget(e.target.value)} />
        </label>
        <label className={field}>
          <span className={labelCls}>Point value ($/pt)</span>
          <input type="number" step="any" className={inputCls} value={pointValue} onChange={(e) => setPointValue(e.target.value)} />
        </label>
        <label className={field}>
          <span className={labelCls}>Account balance</span>
          <input type="number" step="any" className={inputCls} value={balance} onChange={(e) => setBalance(e.target.value)} />
        </label>
        <label className={field}>
          <span className={labelCls}>Strategy / setup</span>
          <input className={inputCls} value={strategy} onChange={(e) => setStrategy(e.target.value)} placeholder="pmVAL retest" />
        </label>
        <label className={field}>
          <span className={labelCls}>Signal</span>
          <select className={inputCls} value={signal} onChange={(e) => setSignal(e.target.value)}>
            <option value="">None</option>
            {KNOWN_SIGNALS.filter(Boolean).map((s) => (
              <option key={s} value={s}>
                {s.replace(/_/g, " ")}
              </option>
            ))}
          </select>
        </label>
        <label className={field}>
          <span className={labelCls}>Tags (comma)</span>
          <input className={inputCls} value={tags} onChange={(e) => setTags(e.target.value)} placeholder="CPI, NY open" />
        </label>
      </div>

      <div className="bg-zinc-800/30 border border-zinc-800 rounded-lg p-4">
        <ExitsEditor
          value={exits}
          onChange={setExits}
          direction={direction}
          entry={entryPrice}
          stop={stopLoss}
          pointValue={pointValue}
        />
      </div>

      <label className={field}>
        <span className={labelCls}>Notes</span>
        <textarea className={`${inputCls} min-h-[70px]`} value={notes} onChange={(e) => setNotes(e.target.value)} />
      </label>

      {showReview && (
        <div className="grid grid-cols-1 sm:grid-cols-[1fr_auto] gap-3">
          <label className={field}>
            <span className={labelCls}>Review (how it was managed, what to improve)</span>
            <textarea className={`${inputCls} min-h-[90px]`} value={review} onChange={(e) => setReview(e.target.value)} />
          </label>
          <label className={field}>
            <span className={labelCls}>Grade</span>
            <select className={inputCls} value={grade} onChange={(e) => setGrade(e.target.value)}>
              <option value="">-</option>
              <option value="A">A</option>
              <option value="B">B</option>
              <option value="C">C</option>
            </select>
          </label>
        </div>
      )}

      {error && <div className="text-sm text-red-400">{error}</div>}

      <button
        type="submit"
        disabled={submitting}
        className="bg-zinc-200 text-zinc-900 hover:bg-white disabled:opacity-50 font-medium rounded-lg px-5 py-2 text-sm"
      >
        {submitting ? "Saving..." : submitLabel}
      </button>
    </form>
  );
}
