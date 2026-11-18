import { numOrNull } from "../utils/tradeForm";

export interface ExitDraft {
  exit_price: string;
  quantity: string;
  exit_time: string;
  fees: string;
  pnl: string;
  note: string;
}

export function emptyExit(): ExitDraft {
  return { exit_price: "", quantity: "", exit_time: "", fees: "", pnl: "", note: "" };
}

function rFor(direction: string, entry: number | null, stop: number | null, exit: number | null): number | null {
  if (entry === null || stop === null || exit === null) return null;
  const long = direction.toLowerCase().startsWith("l");
  const risk = long ? entry - stop : stop - entry;
  const reward = long ? exit - entry : entry - exit;
  if (risk <= 0) return null;
  return reward / risk;
}

interface Props {
  value: ExitDraft[];
  onChange: (rows: ExitDraft[]) => void;
  direction: string;
  entry: string;
  stop: string;
  pointValue: string;
}

const inputCls =
  "w-full bg-zinc-800 border border-zinc-700 rounded px-2 py-1 text-sm text-zinc-200 focus:outline-none focus:border-zinc-500";

export default function ExitsEditor({ value, onChange, direction, entry, stop, pointValue }: Props) {
  const entryN = numOrNull(entry);
  const stopN = numOrNull(stop);
  const pvN = numOrNull(pointValue);
  const totalQty = value.reduce((s, r) => s + (numOrNull(r.quantity) ?? 0), 0);

  const update = (i: number, patch: Partial<ExitDraft>) => {
    onChange(value.map((row, idx) => (idx === i ? { ...row, ...patch } : row)));
  };
  const remove = (i: number) => onChange(value.filter((_, idx) => idx !== i));
  const add = () => onChange([...value, emptyExit()]);

  return (
    <div className="space-y-2">
      <div className="flex items-center justify-between">
        <h3 className="text-sm text-zinc-300 font-medium">Exits (scale-outs)</h3>
        <span className="text-xs text-zinc-500">
          total qty {totalQty || 0}
        </span>
      </div>

      {value.length === 0 && (
        <div className="text-xs text-zinc-500 py-2">
          No exits yet. Add one tranche per scale-out.
        </div>
      )}

      {value.map((row, i) => {
        const exitN = numOrNull(row.exit_price);
        const qtyN = numOrNull(row.quantity);
        const r = rFor(direction, entryN, stopN, exitN);
        const alloc = totalQty > 0 && qtyN !== null ? (qtyN / totalQty) * 100 : null;
        const long = direction.toLowerCase().startsWith("l");
        const pnlPreview =
          pvN !== null && entryN !== null && exitN !== null && qtyN !== null
            ? (long ? exitN - entryN : entryN - exitN) * qtyN * pvN - (numOrNull(row.fees) ?? 0)
            : null;
        return (
          <div key={i} className="bg-zinc-800/40 border border-zinc-700 rounded-lg p-3 space-y-2">
            <div className="grid grid-cols-2 sm:grid-cols-4 gap-2">
              <label className="block">
                <span className="text-xs text-zinc-500">Exit price</span>
                <input
                  type="number"
                  step="any"
                  className={inputCls}
                  value={row.exit_price}
                  onChange={(e) => update(i, { exit_price: e.target.value })}
                />
              </label>
              <label className="block">
                <span className="text-xs text-zinc-500">Quantity</span>
                <input
                  type="number"
                  step="any"
                  className={inputCls}
                  value={row.quantity}
                  onChange={(e) => update(i, { quantity: e.target.value })}
                />
              </label>
              <label className="block">
                <span className="text-xs text-zinc-500">Exit time</span>
                <input
                  type="datetime-local"
                  className={inputCls}
                  value={row.exit_time}
                  onChange={(e) => update(i, { exit_time: e.target.value })}
                />
              </label>
              <label className="block">
                <span className="text-xs text-zinc-500">Fees</span>
                <input
                  type="number"
                  step="any"
                  className={inputCls}
                  value={row.fees}
                  onChange={(e) => update(i, { fees: e.target.value })}
                />
              </label>
            </div>
            <div className="grid grid-cols-2 sm:grid-cols-4 gap-2 items-end">
              <label className="block">
                <span className="text-xs text-zinc-500">P&L (optional)</span>
                <input
                  type="number"
                  step="any"
                  placeholder={pnlPreview !== null ? pnlPreview.toFixed(2) : "auto"}
                  className={inputCls}
                  value={row.pnl}
                  onChange={(e) => update(i, { pnl: e.target.value })}
                />
              </label>
              <label className="block sm:col-span-2">
                <span className="text-xs text-zinc-500">Note</span>
                <input
                  type="text"
                  className={inputCls}
                  value={row.note}
                  onChange={(e) => update(i, { note: e.target.value })}
                />
              </label>
              <button
                type="button"
                onClick={() => remove(i)}
                className="text-xs text-red-400 hover:text-red-300 py-1.5"
              >
                Remove
              </button>
            </div>
            <div className="flex gap-4 text-xs text-zinc-400">
              <span>
                R:{" "}
                <span className={r === null ? "text-zinc-500" : r >= 0 ? "text-green-400" : "text-red-400"}>
                  {r === null ? "needs stop" : r.toFixed(2)}
                </span>
              </span>
              <span>allocation: {alloc === null ? "-" : `${alloc.toFixed(0)}%`}</span>
              {pnlPreview !== null && (
                <span>
                  est. P&L:{" "}
                  <span className={pnlPreview >= 0 ? "text-green-400" : "text-red-400"}>
                    {pnlPreview.toFixed(2)}
                  </span>
                </span>
              )}
            </div>
          </div>
        );
      })}

      <button
        type="button"
        onClick={add}
        className="text-sm text-zinc-300 hover:text-zinc-100 border border-zinc-700 hover:border-zinc-500 rounded px-3 py-1.5"
      >
        + Add exit
      </button>
    </div>
  );
}
