# import sys; sys.path.append('../../04-decode-archives/python')
from game_specs import familymgr

# get Black family
family = familymgr.getFamily('Black')
print(f'studio: {family.studio}')

# get pak with game:/uri
pakFile = family.openPakFile('game:/master.dat#Fallout2')
# pakFile = family.openPakFile('game:/MASTER.DAT#Fallout')
print(f'pak: {pakFile}')

# get pak with resource
# pakFile1 = family.openPakFile(family.parseResource('game:/MASTER.DAT#Fallout'))
# print(f'pak: {pakFile1}')
