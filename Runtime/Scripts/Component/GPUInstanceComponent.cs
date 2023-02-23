using System.Collections.Generic;
using GPUInstanceManager.Component.Base;
using UnityEngine;
using UnityEngine.Rendering;

namespace GPUInstanceManager.Component
{
    public class GPUInstanceComponent : GpuInstanceComponentBase
    {
        [SerializeField] private ShadowCastingMode 
            _castShadows = ShadowCastingMode.On;
        [SerializeField] private bool _receiveShadows = true;
        protected override void SendInstanceDataToManager()
        {
            UpdateCurrentData();
            GpuInstanceManager.AddOrUpdateInstanceData(_targetMeshPair, _targetMaterials,
                new KeyValuePair<int, GPUInstanceData>(_targetTransform.GetInstanceID(), _currentData),
                _castShadows, _receiveShadows);
        }

        protected override void RemoveInstanceDataFromManager()
        {
            GpuInstanceManager.RemoveInstanceData(_targetMeshPair, _targetTransform.GetInstanceID());
        }
        
        private void UpdateCurrentData()
        {
            _currentData.SetRotation(_targetTransform.rotation);
            _currentData.SetPosition(_targetTransform.position);
            _currentData.SetScale(_targetTransform.lossyScale);
            _currentData.UpdateMatrix();
        }

        private void UpdateShadowValue()
        {
            if (!Application.isPlaying)
                return;
            SendInstanceDataToManager();
        }

        private void OnValidate()
        {
            UpdateShadowValue();
        }
    }
}