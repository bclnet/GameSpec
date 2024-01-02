import os, pathlib
from typing import Any
from .openstk_poly import findType

class FileSystem:
    def findPaths(self, path: str, searchPattern: str):
        if (expandStartIdx := searchPattern.find('(')) != -1 and \
            (expandMidIdx := searchPattern.find(':', expandStartIdx)) != -1 and \
            (expandEndIdx := searchPattern.find(')', expandMidIdx)) != -1 and \
            expandStartIdx < expandEndIdx:
            for expand in searchPattern[expandStartIdx + 1: expandEndIdx].split(':'):
                for found in self.findPaths(path, searchPattern[:expandStartIdx] + expand + searchPattern[expandEndIdx+1:]): yield found
            return
        for path in self.glob(path, searchPattern): yield path

class StandardFileSystem(FileSystem):
    def __init__(self, root): self.root = root; self.skip = len(root) + 1
    def glob(self, path: str, searchPattern: str):
        g = pathlib.Path(os.path.join(self.root, path)).glob(searchPattern)
        return [str(x)[self.skip:] for x in g]
    def fileExists(self, path: str) -> bool: return os.path.exists(os.path.join(self.root, path))
    def fileInfo(self, path: str) -> Any: return os.stat(os.path.join(self.root, path))
    def openReader(self, path: str, mode: str = 'rb'): return open(os.path.join(self.root, path), mode) 

class HostFileSystem(FileSystem):
    def __init__(self, uri): self.uri = uri
    def glob(self, path: str, searchPattern: str): raise Exception('Not Implemented')
    def fileExists(self, path: str) -> bool: raise Exception('Not Implemented')
    def fileInfo(self, path: str) -> Any: raise Exception('Not Implemented')
    def openReader(self, path: str, mode: str = 'rb'): raise Exception('Not Implemented')

# create FileSystem
@staticmethod
def createFileSystem(fileSystemType: str, root: str, host = None) -> FileSystem:
    return HostFileSystem(host) if host else \
        findType(fileSystemType)(root) if fileSystemType else \
        StandardFileSystem(root)