const SIGNAL_COLORS: Record<string, string> = {
  COT_Bull: "#22c55e",
  COT_Bear: "#ef4444",
  COT_1Candle_Bull: "#64b464",
  COT_1Candle_Bear: "#b46464",
  MCoC_Bull: "#06b6d4",
  MCoC_Bear: "#d946ef",
  COC_Bull: "#3b82f6",
  COC_Bear: "#a855f7",
  Moon_Bull: "#00ffc8",
  Moon_Bear: "#ff00c8",
  Unknown: "#71717a",
  None: "#71717a",
};

export function signalColor(signal: string | null): string {
  return SIGNAL_COLORS[signal ?? "None"] ?? "#71717a";
}

const SIGNAL_BG: Record<string, string> = {
  COT_Bull: "bg-green-900/40",
  COT_Bear: "bg-red-900/40",
  COT_1Candle_Bull: "bg-green-900/25",
  COT_1Candle_Bear: "bg-red-900/25",
  MCoC_Bull: "bg-cyan-900/40",
  MCoC_Bear: "bg-fuchsia-900/40",
  COC_Bull: "bg-blue-900/40",
  COC_Bear: "bg-purple-900/40",
  Moon_Bull: "bg-emerald-900/40",
  Moon_Bear: "bg-pink-900/40",
};

export function signalBg(signal: string | null): string {
  return SIGNAL_BG[signal ?? ""] ?? "bg-zinc-800/40";
}
