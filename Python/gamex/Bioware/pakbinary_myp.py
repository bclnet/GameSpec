import os
from io import BytesIO
from gamex.filesrc import FileSource
from gamex.pak import PakBinaryT

# typedefs
class Reader: pass
class BinaryPakFile: pass

# PakBinary_Myp
class PakBinary_Myp(PakBinaryT):

    # read
    def read(self, source: BinaryPakFile, r: Reader, tag: object = None) -> None:
        raise Exception('BAD MAGIC')
