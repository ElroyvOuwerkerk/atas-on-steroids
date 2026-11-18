import { useNavigate } from "react-router-dom";
import { useCreateV2Trade } from "../api/tradesV2";
import TradeForm from "../components/TradeForm";

export default function NewTrade() {
  const nav = useNavigate();
  const create = useCreateV2Trade();

  return (
    <div className="space-y-5">
      <h1 className="text-xl font-semibold text-zinc-100">New Trade</h1>
      <p className="text-sm text-zinc-500">
        Log the entry, the stop, and each scale-out. R comes from entry, stop and exit price,
        so it does not need contract size. Point value is only for the money P&L.
      </p>
      <TradeForm
        submitLabel="Create trade"
        submitting={create.isPending}
        onSubmit={(payload) =>
          create.mutate(payload, { onSuccess: (t) => nav(`/journal/${t.id}`) })
        }
      />
      {create.isError && (
        <div className="text-sm text-red-400">
          Failed to save. Is the backend running on port 3001?
        </div>
      )}
    </div>
  );
}
