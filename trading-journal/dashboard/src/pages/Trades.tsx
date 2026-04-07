import { useState } from "react";
import { useTrades } from "../api/trades";
import TradeTable from "../components/TradeTable";
import FilterBar from "../components/FilterBar";

export default function Trades() {
  const [signal, setSignal] = useState("");
  const [from, setFrom] = useState("");
  const [to, setTo] = useState("");

  const { data: trades, isLoading, error } = useTrades({ signal, from, to });

  return (
    <div>
      <h1 className="text-xl font-semibold text-zinc-100 mb-4">Trades</h1>

      <FilterBar
        signal={signal}
        onSignalChange={setSignal}
        fromDate={from}
        onFromChange={setFrom}
        toDate={to}
        onToChange={setTo}
      />

      {isLoading && <div className="text-zinc-500 py-8 text-center">Loading...</div>}
      {error && (
        <div className="text-red-400 py-8 text-center">
          Failed to load trades. Is the backend running on port 3001?
        </div>
      )}
      {trades && <TradeTable trades={trades} />}
    </div>
  );
}
