import os
from typing import Any

def _throw(message: str) -> None:
    raise Exception(message)

def _pathExtension(path: str) -> str:
    return os.path.splitext(path)[1]