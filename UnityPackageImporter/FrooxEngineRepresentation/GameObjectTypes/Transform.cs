﻿using Elements.Core;
using FrooxEngine;
using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityPackageImporter.Models;

namespace UnityPackageImporter.FrooxEngineRepresentation.GameObjectTypes
{

    [Serializable]
    public class Transform: IUnityObject
    {
        
        public TransformFloat4 m_LocalRotation;
        public TransformFloat3 m_LocalPosition;
        public TransformFloat3 m_LocalScale;
        
        
        public ulong id { get; set; }
        public int m_RootOrder;
        public bool instanciated {  get; set; }
        
        public Dictionary<string, ulong> m_PrefabInstance { get; set; }

        public ulong m_FatherID;
        public ulong m_GameObjectID;
        public Dictionary<string, ulong> m_GameObject;
        public Dictionary<string, ulong> m_Father;
        public GameObject parentHashedGameObj;

        public SourceObj m_CorrespondingSourceObject { get; set; }

        public Transform()
        {
        }

        //this is the magic that allows us to construct an entire game object prefab with just yaml parsing.
        public async Task instanciateAsync(IUnityStructureImporter importer)
        {
            if (!instanciated)
            {
                instanciated = true;
                
                if (m_CorrespondingSourceObject.fileID != 0)
                {
                    await createSelf(importer);
                }
                else
                {
                    if (importer.existingIUnityObjects.TryGetValue(m_PrefabInstance["fileID"], out IUnityObject PrefabInstanceObject))
                    {
                        PrefabInstance prefab = PrefabInstanceObject as PrefabInstance;

                        if (!prefab.PrefabHashes.TryGetValue(m_CorrespondingSourceObject, out IUnityObject targetObj)) {

                            Transform alreadydefined = (targetObj as Transform);
                            this.m_LocalRotation = alreadydefined.m_LocalRotation;
                            this.m_LocalPosition = alreadydefined.m_LocalPosition;
                            this.m_LocalScale = alreadydefined.m_LocalScale;

                            this.parentHashedGameObj.frooxEngineSlot.LocalPosition = new float3(m_LocalPosition.x, m_LocalPosition.y, m_LocalPosition.z);
                            this.parentHashedGameObj.frooxEngineSlot.LocalRotation = new floatQ(m_LocalRotation.x, m_LocalRotation.y, m_LocalRotation.z, m_LocalRotation.w);
                            this.parentHashedGameObj.frooxEngineSlot.LocalScale = new float3(m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
                        }
                    }

                    

                }



            }



        }

        private async Task createSelf(IUnityStructureImporter importer)
        {
            if (importer.existingIUnityObjects.TryGetValue(m_GameObjectID, out IUnityObject foundobject) && foundobject.GetType() == typeof(GameObject))
            {
                m_FatherID = m_Father["fileID"];
                m_GameObjectID = m_GameObject["fileID"];
                GameObject parentobj;
                parentobj = foundobject as GameObject;
                await parentobj.instanciateAsync(importer);

                //heh the dictionary stuff in yamls are weird
                await default(ToWorld);
                parentobj.frooxEngineSlot.LocalPosition = new float3(m_LocalPosition.x, m_LocalPosition.y, m_LocalPosition.z);
                parentobj.frooxEngineSlot.LocalRotation = new floatQ(m_LocalRotation.x, m_LocalRotation.y, m_LocalRotation.z, m_LocalRotation.w);
                parentobj.frooxEngineSlot.LocalScale = new float3(m_LocalScale.x, m_LocalScale.y, m_LocalScale.z);
                await default(ToBackground);
                if (importer.existingIUnityObjects.TryGetValue(m_FatherID, out IUnityObject foundobjectparent) && foundobjectparent.GetType() == typeof(Transform))
                {
                    Transform parentTransform = foundobjectparent as Transform;

                    await parentTransform.instanciateAsync(importer);
                    if (importer.existingIUnityObjects.TryGetValue(parentTransform.m_GameObjectID, out IUnityObject parentobj_parent) && parentobj_parent.GetType() == typeof(GameObject))
                    {
                        await default(ToWorld);
                        parentobj.frooxEngineSlot.SetParent((parentobj_parent as GameObject).frooxEngineSlot, false);
                        await default(ToBackground);
                    }
                    else if(parentTransform.parentHashedGameObj != null)
                    {
                        //if this transform was instanciated through the FileImportTaskScene class, we parent ourselves to it's game object here, which is filled by the FileImportTaskScene class.
                        await default(ToWorld);
                        parentobj.frooxEngineSlot.SetParent(parentTransform.parentHashedGameObj.frooxEngineSlot, false);
                        await default(ToBackground);
                    }
                    else
                    {
                        UnityPackageImporter.Warn("The prefab is malformed!!! the transform with an id \"" + id.ToString() + "\" did not find it's parent transform's game object! this will split your import in half heiarchy wise!");
                    }
                }
                else
                {

                    UnityPackageImporter.Warn("Slot did not find it's parent. If there is more than one of these messages in an import then that is bad! Transform id: " + m_FatherID.ToString());
                }
            }
            else
            {
                UnityPackageImporter.Warn("The prefab is malformed!!! the transform with an id \"" + id.ToString() + "\" did not find it's game object! ");

            }
        }

        //a detailed to string for debugging.
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("id: " + id.ToString());
            result.AppendLine("instanciated: " + instanciated.ToString());
            result.AppendLine("m_GameObjectID: " + m_GameObjectID.ToString());
            result.AppendLine("m_GameObject: " + m_GameObject.ToString());
            result.AppendLine("m_Father: " + m_Father.ToString());
            result.AppendLine("m_FatherID: " + m_FatherID.ToString());
            result.AppendLine("m_LocalRotation: " + m_LocalRotation.ToString());
            result.AppendLine("m_LocalPosition: " + m_LocalPosition.ToString());
            result.AppendLine("m_LocalScale: " + m_LocalScale.ToString());
            result.AppendLine("m_CorrespondingSourceObject" + m_CorrespondingSourceObject.ToString());
            result.AppendLine("m_PrefabInstance: " + m_PrefabInstance.ToArrayString());
            


            return result.ToString();
        }

        public async Task UpdateSelf(IUnityStructureImporter importer)
        {
            await createSelf(importer);
        }
    }

    public class TransformFloat4
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("x: " + x.ToString());
            result.AppendLine("y: " + y.ToString());
            result.AppendLine("z: " + z.ToString());
            result.AppendLine("w: " + w.ToString());

            return result.ToString();
        }
    }

    public class TransformFloat3
    {
        public float x;
        public float y;
        public float z;
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("x: " + x.ToString());
            result.AppendLine("y: " + y.ToString());
            result.AppendLine("z: " + z.ToString());

            return result.ToString();
        }
    }
}
