import os
from io import BytesIO
from typing import Any
from ..pakbinary import PakBinary
from ..pakfile import FileSource, BinaryPakFile
from ..openstk_poly import Reader
from ..compression import decompressBlast

class PakBinary_Danae(PakBinary):
    _instance = None
    def __new__(cls):
        if cls._instance is None: cls._instance = super().__new__(cls)
        return cls._instance

    # read
    def read(self, source: BinaryPakFile, r: Reader, tag: Any = None) -> None:
        source.files = files = []
        key = source.game.key; keyLength = len(key); keyIndex = 0

        # move to fat table
        r.seek(r.readUInt32())
        fatSize = r.readUInt32()
        fatBytes = bytearray(r.read(fatSize)); b = 0

        # read int32
        def readInt32() -> int:
            nonlocal b, keyIndex
            p = b
            fatBytes[p + 0] = fatBytes[p + 0] ^ key[keyIndex]; keyIndex += 1
            if keyIndex >= keyLength: keyIndex = 0
            fatBytes[p + 1] = fatBytes[p + 1] ^ key[keyIndex]; keyIndex += 1
            if keyIndex >= keyLength: keyIndex = 0
            fatBytes[p + 2] = fatBytes[p + 2] ^ key[keyIndex]; keyIndex += 1
            if keyIndex >= keyLength: keyIndex = 0
            fatBytes[p + 3] = fatBytes[p + 3] ^ key[keyIndex]; keyIndex += 1
            if keyIndex >= keyLength: keyIndex = 0
            b += 4
            return int.from_bytes(fatBytes[p:p+4], 'little', signed=True)

        # read string
        def readString() -> str:
            nonlocal b, keyIndex
            p = b
            while True:
                fatBytes[p] = fatBytes[p] ^ key[keyIndex]; keyIndex += 1
                if keyIndex >= keyLength: keyIndex = 0
                if fatBytes[p] == 0: break
                p += 1
            length = p - b
            r = fatBytes[b:p].decode('ascii', 'replace')
            b = p + 1
            return r

        # while there are bytes
        while b < fatSize:
            dirPath = readString().replace('\\', '/')
            numFiles = readInt32()
            for _ in range(numFiles):
                # get file
                file = FileSource(
                    path = dirPath + readString().replace('\\', '/'),
                    position = readInt32(),
                    compressed = readInt32(),
                    fileSize = readInt32(),
                    packedSize = readInt32()
                    )
                # special case
                if file.path.endswith('.FTL'): file.compressed = 1
                elif file.compressed == 0: file.fileSize = file.packedSize
                # add file
                files.append(file)

    # readData
    def readData(self, source: BinaryPakFile, r: Reader, file: FileSource) -> BytesIO:
        r.seek(file.position)
        return BytesIO(
            decompressBlast(r, file.packedSize, file.fileSize) if (file.compressed & 1) != 0 else \
            r.read(file.packedSize)
            )