interface Props {
  signal: string;
  onSignalChange: (v: string) => void;
  fromDate: string;
  onFromChange: (v: string) => void;
  toDate: string;
  onToChange: (v: string) => void;
}

const SIGNALS = [
  "",
  "COT_Bull",
  "COT_Bear",
  "COT_1Candle_Bull",
  "COT_1Candle_Bear",
  "MCoC_Bull",
  "MCoC_Bear",
  "COC_Bull",
  "COC_Bear",
  "Moon_Bull",
  "Moon_Bear",
];

export default function FilterBar({
  signal,
  onSignalChange,
  fromDate,
  onFromChange,
  toDate,
  onToChange,
}: Props) {
  return (
    <div className="flex flex-wrap gap-3 items-center mb-4">
      <select
        value={signal}
        onChange={(e) => onSignalChange(e.target.value)}
        className="bg-zinc-800 border border-zinc-700 rounded px-3 py-1.5 text-sm text-zinc-200"
      >
        <option value="">All Signals</option>
        {SIGNALS.filter(Boolean).map((s) => (
          <option key={s} value={s}>
            {s.replace(/_/g, " ")}
          </option>
        ))}
      </select>

      <input
        type="date"
        value={fromDate}
        onChange={(e) => onFromChange(e.target.value)}
        placeholder="From"
        className="bg-zinc-800 border border-zinc-700 rounded px-3 py-1.5 text-sm text-zinc-200"
      />

      <input
        type="date"
        value={toDate}
        onChange={(e) => onToChange(e.target.value)}
        placeholder="To"
        className="bg-zinc-800 border border-zinc-700 rounded px-3 py-1.5 text-sm text-zinc-200"
      />

      {(signal || fromDate || toDate) && (
        <button
          onClick={() => {
            onSignalChange("");
            onFromChange("");
            onToChange("");
          }}
          className="text-xs text-zinc-400 hover:text-zinc-200 underline"
        >
          Clear filters
        </button>
      )}
    </div>
  );
}
