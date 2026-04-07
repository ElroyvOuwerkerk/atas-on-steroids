import { useState, useEffect, useRef } from "react";
import { useUpdateNotes } from "../api/trades";

export default function NotesEditor({
  tradeId,
  initialNotes,
}: {
  tradeId: number;
  initialNotes: string | null;
}) {
  const [value, setValue] = useState(initialNotes ?? "");
  const mutation = useUpdateNotes(tradeId);
  const timerRef = useRef<ReturnType<typeof setTimeout>>(undefined);

  useEffect(() => {
    setValue(initialNotes ?? "");
  }, [initialNotes]);

  function handleChange(text: string) {
    setValue(text);
    if (timerRef.current) clearTimeout(timerRef.current);
    timerRef.current = setTimeout(() => {
      mutation.mutate(text);
    }, 800);
  }

  return (
    <div>
      <label className="block text-sm text-zinc-400 mb-1">Notes</label>
      <textarea
        value={value}
        onChange={(e) => handleChange(e.target.value)}
        placeholder="Add notes about this trade..."
        rows={5}
        className="w-full bg-zinc-800 border border-zinc-700 rounded-lg px-3 py-2 text-sm text-zinc-200 resize-y focus:outline-none focus:border-zinc-500"
      />
      {mutation.isPending && (
        <span className="text-xs text-zinc-500">Saving...</span>
      )}
      {mutation.isSuccess && (
        <span className="text-xs text-green-500">Saved</span>
      )}
    </div>
  );
}
