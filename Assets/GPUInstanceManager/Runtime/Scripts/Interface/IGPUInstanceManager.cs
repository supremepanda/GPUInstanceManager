using System.Collections.Generic;
using GPUInstanceManager.Component;
using UnityEngine;
using UnityEngine.Rendering;

namespace GPUInstanceManager.Interface
{
    public interface IGPUInstanceManager
    {
        public void AddOrUpdateInstanceData(KeyValuePair<int, Mesh> idMeshPair, Material[] material,
            KeyValuePair<int, GPUInstanceData> instanceData, ShadowCastingMode castShadows, bool receiveShadows);
        public void RemoveInstanceData(KeyValuePair<int, Mesh> idMeshPair, int instanceId);
    }
}