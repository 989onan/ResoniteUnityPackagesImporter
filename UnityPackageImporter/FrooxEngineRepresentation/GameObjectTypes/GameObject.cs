﻿using FrooxEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnityPackageImporter.FrooxEngineRepresentation.GameObjectTypes
{
    [Serializable]
    public class GameObject: IUnityObject
    {

        public string m_Name;
        public int m_IsActive = 1;
        public bool instanciated { get; set; }
        public ulong id { get; set; }

        public Slot frooxEngineSlot;

        public GameObject() {
        }


        public async Task instanciateAsync(Dictionary<ulong, IUnityObject> existing_prefab_entries)
        {
            if (!instanciated)
            {
                instanciated = true;
                await default(ToWorld);
                frooxEngineSlot = Engine.Current.WorldManager.FocusedWorld.AddSlot(this.m_Name);
                frooxEngineSlot.ActiveSelf = m_IsActive == 1 ? true : false;
                await default(ToBackground);
            }
            
        }

        //a detailed to string for debugging.
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("id: " + id.ToString());
            result.AppendLine("instanciated: " + instanciated.ToString());
            result.AppendLine("m_IsActive: "+ m_IsActive.ToString());
            result.AppendLine("m_Name: " + m_Name.ToString());
            result.AppendLine("frooxEngineSlot: " + frooxEngineSlot.ToString());

            return result.ToString();
        }
    }
}