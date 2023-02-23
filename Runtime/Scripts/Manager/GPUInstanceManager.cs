using System.Collections.Generic;
using GPUInstanceManager.Component;
using GPUInstanceManager.Manager.Base;
using UnityEngine;
using UnityEngine.Rendering;

namespace GPUInstanceManager.Manager
{
    public class GPUInstanceManager : GpuInstanceManagerBase
    {
        public override void AddOrUpdateInstanceData(KeyValuePair<int, Mesh> idMeshPair, Material[] material, 
            KeyValuePair<int, GPUInstanceData> instanceData, ShadowCastingMode castShadows, bool receiveShadows)
        {
            if(_meshes.ContainsKey(idMeshPair.Key))
            {
                _meshes[idMeshPair.Key] = idMeshPair.Value;
            }
            else
            {
                _meshes.Add(idMeshPair.Key, idMeshPair.Value);
            }
            if (_gpuInstanceData.ContainsKey(idMeshPair))
            {
                var gpuInstanceManagerData = _gpuInstanceData[idMeshPair];
                var instance = gpuInstanceManagerData.InstanceData;
                var instanceId = instanceData.Key;
                var targetGPUInstanceData = instanceData.Value;
                if (instance.ContainsKey(instanceId))
                    instance[instanceId] = targetGPUInstanceData;
                else
                    instance.Add(instanceId, targetGPUInstanceData);
                gpuInstanceManagerData.IsChanged = true;
                if(gpuInstanceManagerData.CastShadows != castShadows)
                    gpuInstanceManagerData.SetCastShadows(castShadows);
                if(gpuInstanceManagerData.ReceiveShadows != receiveShadows)
                    gpuInstanceManagerData.SetReceiveShadows(receiveShadows);
            }
            else
            {
                _gpuInstanceData.Add(idMeshPair, new GPUInstanceManagerData(
                    new Dictionary<int, GPUInstanceData> {{instanceData.Key, instanceData.Value}},
                    material, new Matrix4x4[128], castShadows, receiveShadows));
            }
        }

        public override void RemoveInstanceData(KeyValuePair<int, Mesh> idMeshPair, int instanceId)
        {
            var gpuInstanceManagerData = _gpuInstanceData[idMeshPair];
            var data = gpuInstanceManagerData.InstanceData;
            if (!data.ContainsKey(instanceId)) 
                return;
            data.Remove(instanceId);
            gpuInstanceManagerData.IsChanged = true;
        }

        protected override void RenderBatches()
        {
            foreach (var gpuInstanceData in _gpuInstanceData)
            {
                var gpuInstanceManagerData = gpuInstanceData.Value;
                var meshPair = gpuInstanceData.Key;
                if (gpuInstanceManagerData.IsChanged)
                {
                    GetMatricesFromInstances(meshPair);
                    gpuInstanceManagerData.IsChanged = false;
                }

                for (int i = 0; i < gpuInstanceManagerData.Material.Length; i++)
                {
                    Graphics.DrawMeshInstanced(mesh: meshPair.Value, submeshIndex: i, material: gpuInstanceManagerData.Material[i],
                        matrices: gpuInstanceManagerData.Matrices, count: gpuInstanceManagerData.InstanceData.Count,
                        null, gpuInstanceManagerData.CastShadows, gpuInstanceManagerData.ReceiveShadows);
                }
            }
        }

        private void GetMatricesFromInstances(KeyValuePair<int, Mesh> meshPair)
        {
            var index = 0;
            var matrices = _gpuInstanceData[meshPair].Matrices;
            var data = _gpuInstanceData[meshPair].InstanceData.Values;
            if(data.Count > matrices.Length)
            {
                var length = matrices.Length;
                do { length *= 2; }
                while (length < data.Count) ;
                _gpuInstanceData[meshPair].Matrices = new Matrix4x4[length];
                return;
            }
            foreach (var value in data)
            {
                matrices[index] = value.Matrix;
                index++;
            }
        }
    }
}