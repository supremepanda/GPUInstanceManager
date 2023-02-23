using System.Collections.Generic;
using GPUInstanceManager.Component;
using GPUInstanceManager.Interface;
using UnityEngine;
using UnityEngine.Rendering;

namespace GPUInstanceManager.Manager.Base
{
    public abstract class GpuInstanceManagerBase : MonoBehaviour, IGPUInstanceManager
    {
        protected readonly  Dictionary<int, Mesh> _meshes = new Dictionary<int, Mesh>();
        protected readonly Dictionary<KeyValuePair<int, Mesh>, GPUInstanceManagerData> _gpuInstanceData = 
            new Dictionary<KeyValuePair<int, Mesh>, GPUInstanceManagerData>();

        protected virtual void Update()
        {
            RenderBatches();
        }

        protected abstract void RenderBatches();

        public abstract void AddOrUpdateInstanceData(KeyValuePair<int, Mesh> idMeshPair, Material[] material,
            KeyValuePair<int, GPUInstanceData> instanceData, ShadowCastingMode castShadows, bool receiveShadows);
        public abstract void RemoveInstanceData(KeyValuePair<int, Mesh> idMeshPair, int instanceId);
    }
}