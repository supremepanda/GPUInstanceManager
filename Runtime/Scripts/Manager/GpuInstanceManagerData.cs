using System.Collections.Generic;
using GPUInstanceManager.Component;
using UnityEngine;
using UnityEngine.Rendering;

namespace GPUInstanceManager.Manager
{
    public class GPUInstanceManagerData
    {
        public GPUInstanceManagerData(Dictionary<int, GPUInstanceData> instanceData, Material[] material,
            Matrix4x4[] matrices, ShadowCastingMode castShadows, bool receiveShadows)
        {
            this.InstanceData = instanceData;
            this.Material = material;
            this.Matrices = matrices;
            this.IsChanged = true;
            this.CastShadows = castShadows;
            this.ReceiveShadows = receiveShadows;
        }

        public Dictionary<int, GPUInstanceData> InstanceData { get; }
        public Material[] Material { get; }
        public Matrix4x4[] Matrices { get; set; }
        public bool IsChanged { get; set; }
        public ShadowCastingMode CastShadows { get; private set; }
        public bool ReceiveShadows { get; private set; } = true;
        
        public void SetCastShadows(ShadowCastingMode castShadows)
        {
            this.CastShadows = castShadows;
        }
        
        public void SetReceiveShadows(bool receiveShadows)
        {
            this.ReceiveShadows = receiveShadows;
        }
    }
}