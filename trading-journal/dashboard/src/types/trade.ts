export interface Trade {
  id: number;
  instrument: string;
  direction: string;
  entry_price: number;
  exit_price: number;
  size: number;
  pnl: number;
  entry_time: string;
  exit_time: string;
  signal: string | null;
  signal_source: string | null;
  clip_filename: string | null;
  notes: string | null;
  created_at: string | null;
}

export interface SignalStats {
  signal: string;
  total: number;
  wins: number;
  losses: number;
  win_rate: number;
  avg_pnl: number;
  cumulative_pnl: number;
}

export interface StatsResponse {
  overall: SignalStats;
  by_signal: SignalStats[];
}
