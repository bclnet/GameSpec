import os
from io import BytesIO
from gamespecs.pakfile import FileSource, PakBinaryT

# typedefs
class Reader: pass
class BinaryPakFile: pass

# PakBinary_Big
class PakBinary_Big(PakBinaryT):

    # read
    def read(self, source: BinaryPakFile, r: Reader, tag: object = None) -> None:
        raise NotImplementedError()
