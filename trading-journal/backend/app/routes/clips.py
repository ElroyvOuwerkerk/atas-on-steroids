from fastapi import APIRouter, HTTPException
from fastapi.responses import FileResponse

from app.config import OBS_CLIPS_DIR

router = APIRouter(prefix="/api/clips", tags=["clips"])


@router.get("/{filename}")
async def serve_clip(filename: str):
    # Prevent path traversal
    if "/" in filename or "\\" in filename or ".." in filename:
        raise HTTPException(status_code=400, detail="Invalid filename")

    path = OBS_CLIPS_DIR / filename
    if not path.is_file():
        raise HTTPException(status_code=404, detail="Clip not found")

    return FileResponse(path, media_type="video/mp4")
