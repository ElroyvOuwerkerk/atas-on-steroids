// Journal v2 types. Mirror the backend schemas_v2.py.

export interface ExitOut {
  id: number;
  exit_price: number;
  quantity: number;
  exit_time: string | null;
  fees: number | null;
  pnl: number | null;
  note: string | null;
  r_multiple: number | null;
  allocation_pct: number | null;
}

export interface ExitIn {
  exit_price: number;
  quantity: number;
  exit_time?: string | null;
  fees?: number | null;
  pnl?: number | null;
  note?: string | null;
}

export interface TradeV2 {
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
  stop_loss: number | null;
  target: number | null;
  point_value: number | null;
  account_balance_at_entry: number | null;
  strategy: string | null;
  tags: string | null;
  review: string | null;
  grade: string | null;
  exits: ExitOut[];
  realized_r: number | null;
  total_exited_qty: number;
  open_qty: number;
  avg_exit_price: number | null;
  computed_pnl: number | null;
  initial_risk: number | null;
  risk_pct: number | null;
  scaled_out: boolean;
  session: string | null;
  weekday: string | null;
}

export interface TradeV2Create {
  instrument: string;
  direction: string;
  entry_price: number;
  size: number;
  entry_time: string;
  stop_loss?: number | null;
  target?: number | null;
  point_value?: number | null;
  account_balance_at_entry?: number | null;
  strategy?: string | null;
  tags?: string | null;
  signal?: string | null;
  signal_source?: string | null;
  notes?: string | null;
  exits: ExitIn[];
}

export interface TradeV2Update extends Partial<TradeV2Create> {
  review?: string | null;
  grade?: string | null;
}

export interface RBucket {
  key: string;
  trades: number;
  total_r: number;
  avg_r: number;
  win_rate: number;
  net_pnl: number;
}

export interface ReviewResponse {
  trades_with_r: number;
  total_realized_r: number;
  avg_r: number;
  expectancy_r: number;
  scaled_out_share: number;
  by_strategy: RBucket[];
  by_session: RBucket[];
  by_weekday: RBucket[];
  by_hour: RBucket[];
  by_tag: RBucket[];
  r_distribution: RBucket[];
}
