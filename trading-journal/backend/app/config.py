import os
from pathlib import Path

BASE_DIR = Path(__file__).resolve().parent.parent

DATABASE_URL = os.getenv(
    "DATABASE_URL",
    f"sqlite+aiosqlite:///{BASE_DIR / 'journal.db'}",
)

OBS_CLIPS_DIR = Path(
    os.getenv("OBS_CLIPS_DIR", str(Path.home() / "Movies" / "OBS Replay"))
)

CLIP_MATCH_WINDOW_SECONDS = int(os.getenv("CLIP_MATCH_WINDOW_SECONDS", "30"))
