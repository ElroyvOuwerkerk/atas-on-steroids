import { signalColor, signalBg } from "../utils/colors";

export default function SignalBadge({ signal }: { signal: string | null }) {
  const label = signal ?? "None";
  const display = label.replace(/_/g, " ");

  return (
    <span
      className={`inline-block px-2 py-0.5 rounded text-xs font-medium ${signalBg(signal)}`}
      style={{ color: signalColor(signal) }}
    >
      {display}
    </span>
  );
}
