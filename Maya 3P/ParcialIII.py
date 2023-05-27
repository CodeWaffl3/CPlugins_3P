import maya.cmds as cmds
import MASH.api as mapi


# def CreateWindow():
#     if cmds.window('LandscapeCreation', exists=True):
#         cmds.deleteUI('LandscapeCreation')
#     cmds.window('LandscapeCreation')
#     cmds.showWindow('LandscapeCreation')
#
# CreateWindow()
#
# cmds.polyPlane(name='Landscape', subdivisionsY=30, subdivisionsX=30, width=10, height=10)
#
# cmds.textureDeformer(name='HeightMap', envelope=1, strength=4, offset=0, vectorStrength=[1,1,1], vectorOffset=[0,0,0],vectorSpace="Object",direction="Handle",pointSpace="UV",exclusive="")
#
# #2D Texture and FILE
# cmds.shadingNode("place2dTexture", name="MountainTexture", asUtility=True)
# cmds.shadingNode("file", name="TextureFile", asTexture=True, isColorManaged=True)
#
# #2D Texture and FILE CONNECT Attr
# cmds.connectAttr("MountainTexture.coverage", "TextureFile.coverage")
# cmds.connectAttr("MountainTexture.translateFrame", "TextureFile.translateFrame")
# cmds.connectAttr("MountainTexture.rotateFrame", "TextureFile.rotateFrame",)
# cmds.connectAttr("MountainTexture.mirrorU", "TextureFile.mirrorU")
# cmds.connectAttr("MountainTexture.mirrorV", "TextureFile.mirrorV")
# cmds.connectAttr("MountainTexture.stagger", "TextureFile.stagger")
# cmds.connectAttr("MountainTexture.wrapU", "TextureFile.wrapU")
# cmds.connectAttr("MountainTexture.wrapV", "TextureFile.wrapV")
# cmds.connectAttr("MountainTexture.repeatUV", "TextureFile.repeatUV")
# cmds.connectAttr("MountainTexture.offset", "TextureFile.offset")
# cmds.connectAttr("MountainTexture.rotateUV", "TextureFile.rotateUV")
# cmds.connectAttr("MountainTexture.noiseUV", "TextureFile.noiseUV")
# cmds.connectAttr("MountainTexture.vertexUvOne", "TextureFile.vertexUvOne")
# cmds.connectAttr("MountainTexture.vertexUvTwo", "TextureFile.vertexUvTwo")
# cmds.connectAttr("MountainTexture.vertexUvThree", "TextureFile.vertexUvThree")
# cmds.connectAttr("MountainTexture.vertexCameraOne", "TextureFile.vertexCameraOne")
# cmds.connectAttr("MountainTexture.outUV", "TextureFile.uv")
# cmds.connectAttr("MountainTexture.outUvFilterSize", "TextureFile.uvFilterSize")
# cmds.connectAttr("TextureFile.outColor", "HeightMap.texture")
#
# path = "D:/Andres/Documents/MayaProjects/Heightmap.jpeg"
# cmds.setAttr("TextureFile.fileTextureName", path, type="string")
#
# #Importar objeto
pathfile = "D:/Andres/Documents/MayaProjects/Assets for games/Meele/assets/ReconPole.fbx";
fileType = "fbx"

files = cmds.getFileList(folder = pathfile, filespec = '*.%s' % fileType)

if len(files) == 0:
    cmds.warning("NO FILES FOUND")
else:
    for f in files:
        cmds.file(pathfile + f, i = True)



#CONNECTAR EL MASH


#create a new MASH network

cmds.select("ReconPole")
mashNetwork = mapi.Network()
mashNetwork.createNetwork(name="Test1",  geometry="Repro")
shape = "Landscape"
mashNetwork.meshDistribute(shape, 4)
cmds.setAttr("Test1_Distribute.calcRotation", 0)



"""
defaultNavigation -createNew -destination "HeightMap.texture";
createRenderNode -allWithTexturesUp "defaultNavigation -force true -connectToExisting -source %node -destination HeightMap.texture" "";
defaultNavigation -defaultTraversal -destination "HeightMap.texture";
shadingNode -asTexture -isColorManaged file;
"""


