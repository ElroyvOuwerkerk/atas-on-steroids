export default function VideoPlayer({
  filename,
}: {
  filename: string | null;
}) {
  if (!filename) {
    return (
      <div className="bg-zinc-800/50 border border-zinc-700 rounded-lg flex items-center justify-center h-48 text-zinc-500 text-sm">
        No replay clip available
      </div>
    );
  }

  return (
    <video
      controls
      className="w-full rounded-lg border border-zinc-700"
      src={`/api/clips/${encodeURIComponent(filename)}`}
    >
      Your browser does not support video playback.
    </video>
  );
}
