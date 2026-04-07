import { useParams, Link } from "react-router-dom";
import { useTrade } from "../api/trades";
import TradeDetail from "../components/TradeDetail";

export default function TradeView() {
  const { id } = useParams<{ id: string }>();
  const { data: trade, isLoading, error } = useTrade(Number(id));

  return (
    <div>
      <Link
        to="/trades"
        className="text-sm text-zinc-400 hover:text-zinc-200 mb-4 inline-block"
      >
        &larr; Back to trades
      </Link>

      {isLoading && <div className="text-zinc-500 py-8 text-center">Loading...</div>}
      {error && (
        <div className="text-red-400 py-8 text-center">Trade not found.</div>
      )}
      {trade && <TradeDetail trade={trade} />}
    </div>
  );
}
