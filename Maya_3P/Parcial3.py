import maya.cmds as cmds
import MASH.api as mapi
from random import randrange


def Particles():
    cmds.polySphere(r=1, n='sphere')
    cmds.select('sphere')
    cmds.move(0, 5.486749, 0, 'sphere', r=True)

    cmds.emitter(type='omni',name="particlesobj1",
                 rate= 10, sro= 0,nuv= 0, cye= 'none', cyi= 1, spd= 1, srn= 0, nsp= 1,
                 tsp= 0, mxd= 0, mnd= 0,dx= 1, dy= 0, dz= 0, sp= 0)

    cmds.nParticle()
    cmds.connectDynamic(em="particlesobj1")

    cmds.setAttr("nucleus1.gravity", 0)
    cmds.setAttr("nucleus1.windSpeed", 10)
    cmds.setAttr("nParticleShape1.color[0].color_Color", 1, 0, 0, type="double3")
    cmds.select('nParticle1')
    cmds.vortex(pos= [0, 0, 0], m =100, att= 1, ax= 0, ay=1, az= 0, mxd =-1, vsh= 'none', vex= 0, vof= [0 ,0, 0], vsw= 360, tsr= 0.5)
    cmds.connectDynamic("nParticle1", f="vortexField1")


############## VENTANA #######################

class M_Window(object):
    def __init__(self):
        #Type of window Maya Window
        self.window = "M_Window"  # must
        #Title of window
        self.title = "Landscape Creator"  # must
        #Size of window
        self.size = (300, 300)  # must

        if cmds.window(self.window, exists=True):  # must
            cmds.deleteUI(self.window, window=True)  # must

        self.window = cmds.window(self.window, title=self.title, widthHeight=self.size)  # must
        cmds.columnLayout()  # must

        ########################## TAMANO DEL PLANO ####################################

        # Height
        self.Height = cmds.text(l="Height")
        self.HeightValue = cmds.intField(minValue=0, maxValue=100, value=50)

        # Width
        self.Width = cmds.text(l="Width")
        self.WidthValue = cmds.intField(minValue=1, maxValue=100, value=50)

        ########################## OBJETOS ####################################

        # Arboles
        self.Trees = cmds.text(l="Tree count")
        self.TreesCount = cmds.intField(minValue=1, maxValue=300, value=100)

        # Casas
        self.Houses = cmds.text(l="House Count")
        self.HouseCount = cmds.intField(minValue=1, maxValue=20, value=10)

        # ReconPoles
        self.ReconPoles = cmds.text(l="Recon Poles")
        self.ReconPolesCount = cmds.intField(minValue=1, maxValue=15, value=8)

        #Particles 
        self.ParticlesText = cmds.text(l="Particles (1=yes, 0=no)")
        self.ParticlesField = cmds.intField(minValue=0, maxValue=1, value=1)

        self.CreateLandscapeButton = cmds.button(l="Generate Landscape", c=self.createLandScape)

        self.deleteAll = cmds.button(l="Delete All ", c=self.fdeleteAll)

        cmds.showWindow()  # must

    def createLandScape(self, *args):
        Height = cmds.intField(self.HeightValue, query=True, value=True)
        Width = cmds.intField(self.WidthValue, query=True, value=True)
        Trees = cmds.intField(self.TreesCount, query=True, value=True)
        Houses = cmds.intField(self.HouseCount, query=True, value=True)
        ReconPoles = cmds.intField(self.ReconPolesCount, query=True, value=True)
        ParticlesValue = cmds.intField(self.ParticlesField, query=True, value=True)

        #################### CREATE DEFORMER MAP #####################################

        cmds.polyPlane(name='Landscape', subdivisionsY=30, subdivisionsX=30, width=Width, height=Height)

        cmds.textureDeformer(name='HeightMap', envelope=1, strength=9, offset=0, vectorStrength=[1, 1, 1],
                             vectorOffset=[0, 0, 0], vectorSpace="Object", direction="Handle", pointSpace="UV",
                             exclusive="")

        ################### Height map 2D Texture and FILE #################################
        cmds.shadingNode("place2dTexture", name="MountainTexture", asUtility=True)
        cmds.shadingNode("file", name="TextureFile", asTexture=True, isColorManaged=True)

        ########### 2D Texture and FILE CONNECT Attr ##############################
        cmds.connectAttr("MountainTexture.coverage", "TextureFile.coverage")
        cmds.connectAttr("MountainTexture.translateFrame", "TextureFile.translateFrame")
        cmds.connectAttr("MountainTexture.rotateFrame", "TextureFile.rotateFrame", )
        cmds.connectAttr("MountainTexture.mirrorU", "TextureFile.mirrorU")
        cmds.connectAttr("MountainTexture.mirrorV", "TextureFile.mirrorV")
        cmds.connectAttr("MountainTexture.stagger", "TextureFile.stagger")
        cmds.connectAttr("MountainTexture.wrapU", "TextureFile.wrapU")
        cmds.connectAttr("MountainTexture.wrapV", "TextureFile.wrapV")
        cmds.connectAttr("MountainTexture.repeatUV", "TextureFile.repeatUV")
        cmds.connectAttr("MountainTexture.offset", "TextureFile.offset")
        cmds.connectAttr("MountainTexture.rotateUV", "TextureFile.rotateUV")
        cmds.connectAttr("MountainTexture.noiseUV", "TextureFile.noiseUV")
        cmds.connectAttr("MountainTexture.vertexUvOne", "TextureFile.vertexUvOne")
        cmds.connectAttr("MountainTexture.vertexUvTwo", "TextureFile.vertexUvTwo")
        cmds.connectAttr("MountainTexture.vertexUvThree", "TextureFile.vertexUvThree")
        cmds.connectAttr("MountainTexture.vertexCameraOne", "TextureFile.vertexCameraOne")
        cmds.connectAttr("MountainTexture.outUV", "TextureFile.uv")
        cmds.connectAttr("MountainTexture.outUvFilterSize", "TextureFile.uvFilterSize")
        cmds.connectAttr("TextureFile.outColor", "HeightMap.texture")
        path = f"D:/Andres/Documents/MayaProjects/5.jpeg"

        randHeightMap = randrange(1, 5)

        if (randHeightMap == 5):
            path = f"C:/Users/PC/Documents/UP/6S/plugins/CPlugins_3P/Maya_3P/HeightMaps/{randHeightMap}.jpeg"
        else:
            path = f"C:/Users/PC/Documents/UP/6S/plugins/CPlugins_3P/Maya_3P/HeightMaps/{randHeightMap}.jpg"
        cmds.setAttr("TextureFile.fileTextureName", path, type="string")
        cmds.setAttr("HeightMap.strength", 10)



        ######################### CONNECTAR EL MASH ########################################
        shape = "Landscape"

        #### RECON POLES ####

        # importa el objeto
        cmds.file("C:/Users/PC/Documents/UP/6S/plugins/CPlugins_3P/Maya_3P/ObjetosFBX/ReconPole.fbx", i=True)
        cmds.setAttr("ReconPole.scaleX", 0.04)
        cmds.setAttr("ReconPole.scaleY", 0.04)
        cmds.setAttr("ReconPole.scaleZ", 0.04)

        #Hace la MASH network para ponerlos en el mapa
        cmds.select("ReconPole")
        mashNetwork = mapi.Network()
        mashNetwork.createNetwork(name="DistributionsReconPoles", geometry="Repro")
        mashNetwork.meshDistribute(shape, 4)
        cmds.setAttr("DistributionsReconPoles_Distribute.calcRotation", 0)
        cmds.setAttr("DistributionsReconPoles_Distribute.meshType", 1)
        cmds.setAttr("DistributionsReconPoles_Distribute.pointCount", ReconPoles)
        cmds.setAttr("DistributionsReconPoles_Distribute.seed", 6)

        #### CASAS ####

        # Importa el objeto
        cmds.file("C:/Users/PC/Documents/UP/6S/plugins/CPlugins_3P/Maya_3P/ObjetosFBX/House.fbx", i=True)
        cmds.setAttr("House.scaleX", 0.03)
        cmds.setAttr("House.scaleY", 0.03)
        cmds.setAttr("House.scaleZ", 0.03)

        # Hace el MASH network para ponerlos en el mapa
        cmds.select("House")
        mashNetwork2 = mapi.Network()
        mashNetwork2.createNetwork(name="DistributionsHouses", geometry="Repro")
        mashNetwork2.meshDistribute(shape, 8)
        cmds.setAttr("DistributionsHouses_Distribute.calcRotation", 0)
        cmds.setAttr("DistributionsHouses_Distribute.meshType", 1)
        cmds.setAttr("DistributionsHouses_Distribute.pointCount", Houses)
        cmds.setAttr("DistributionsHouses_Distribute.seed", 8)

        #### ARBOLES ####

        # Importa los objetos
        cmds.file("C:/Users/PC/Documents/UP/6S/plugins/CPlugins_3P/Maya_3P/ObjetosFBX/Trees/Trees.ma", i=True)

        # Hace el MASH network para ponerlos en el mapa
        #   For para seleccionar los 12 arboles
        for x in range(1, 13):

            if (x == 1):
                cmds.select(f"Tree{x}", replace=True)

            cmds.select(f"Tree{x}", add=True)
            cmds.setAttr(f"Tree{x}.scaleX", 0.01)
            cmds.setAttr(f"Tree{x}.scaleY", 0.01)
            cmds.setAttr(f"Tree{x}.scaleZ", 0.01)

        mashNetwork3 = mapi.Network()
        mashNetwork3.createNetwork(name="DistributionTrees", geometry="Repro")
        mashNetwork3.meshDistribute(shape, 8)
        cmds.setAttr("DistributionTrees_Distribute.calcRotation", 0)
        cmds.setAttr("DistributionTrees_Distribute.meshType", 1)
        cmds.setAttr("DistributionTrees_Distribute.pointCount", Trees)
        cmds.setAttr("DistributionTrees_Distribute.seed", 100)

        #### PARTICULAS ####
        if ParticlesValue == 1:
            particles = Particles()
        

    def fdeleteAll(self, *args):
        cmds.select(all=True)
        cmds.delete()


myWindow = M_Window()



