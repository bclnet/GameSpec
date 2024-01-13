@staticmethod
def getPathByKey(key):
    k,v = key.split(':', 2)
    match k:
        case 'Steam':
            from .storemgr_steam import steamPaths
            return steamPaths[v] if v in steamPaths else None
        case 'GOG':
            from .storemgr_gog import gogPaths
            return gogPaths[v] if v in gogPaths else None
        case 'Blizzard':
            from .storemgr_blizzard import blizzardPaths
            return blizzardPaths[v] if v in blizzardPaths else None
        case 'Epic':
            from .storemgr_epic import epicPaths
            return epicPaths[v] if v in epicPaths else None
        case 'Ubisoft':
            from .storemgr_ubisoft import ubisoftPaths
            return ubisoftPaths[v] if v in ubisoftPaths else None
        case 'Unknown': return None
        case _: raise Exception(f'Unknown key: {key}')

# print(getPathByKey('Steam:1755910'))
