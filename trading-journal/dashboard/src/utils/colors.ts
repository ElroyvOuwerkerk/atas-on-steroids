const SIGNAL_COLORS: Record<string, string> = {
  COT_Bull: "#22c55e",
  COT_Bear: "#ef4444",
  COT_Weak_Bull: "#64b464",
  COT_Weak_Bear: "#b46464",
  Magic_Bull: "#06b6d4",
  Magic_Bear: "#d946ef",
  COC_Bull: "#3b82f6",
  COC_Bear: "#a855f7",
  Momentum_Bull: "#22c55e",
  Momentum_Bear: "#ef4444",
  Unknown: "#71717a",
  None: "#71717a",
};

export function signalColor(signal: string | null): string {
  return SIGNAL_COLORS[signal ?? "None"] ?? "#71717a";
}

const SIGNAL_BG: Record<string, string> = {
  COT_Bull: "bg-green-900/40",
  COT_Bear: "bg-red-900/40",
  COT_Weak_Bull: "bg-green-900/25",
  COT_Weak_Bear: "bg-red-900/25",
  Magic_Bull: "bg-cyan-900/40",
  Magic_Bear: "bg-fuchsia-900/40",
  COC_Bull: "bg-blue-900/40",
  COC_Bear: "bg-purple-900/40",
  Momentum_Bull: "bg-green-900/25",
  Momentum_Bear: "bg-red-900/25",
};

export function signalBg(signal: string | null): string {
  return SIGNAL_BG[signal ?? ""] ?? "bg-zinc-800/40";
}
