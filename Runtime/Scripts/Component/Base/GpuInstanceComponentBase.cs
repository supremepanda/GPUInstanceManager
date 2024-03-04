using System.Collections.Generic;
using GPUInstanceManager.Interface;
using UnityEngine;

namespace GPUInstanceManager.Component.Base
{
    public abstract class GpuInstanceComponentBase : MonoBehaviour
    {
        [Header("====Auto Find====")]
        [SerializeField] protected bool _autoFindTargetTransform = true;
        [SerializeField] protected bool _autoFindMesh = true;
        [SerializeField] protected bool _autoFindMaterials = true;
        [Space, Header("====Targets====\n(If auto find is enabled, this will be ignored)")]
        [SerializeField] protected Transform _targetTransform;
        [SerializeField] protected Mesh _targetMesh;
        [SerializeField] protected Material[] _targetMaterials;
        [Space, Header("===Configuration===")]
        [SerializeField] protected int _uniqueMeshId;
        protected IGPUInstanceManager GpuInstanceManager { get; private set; }
        protected GPUInstanceData _currentData;
        private bool _isInitialized = false;
        protected KeyValuePair<int, Mesh> _targetMeshPair;

        protected virtual void Reset()
        {
            _targetTransform.GetComponent<MeshRenderer>().enabled = false;
        }

        protected virtual void Awake()
        {
            if (_autoFindTargetTransform)
            {
                AutoFindAndSetTargetTransform();
            }
            if (_autoFindMesh)
            {
                AutoFindAndSetMesh();
            }
            if (_autoFindMaterials)
            {
                AutoFindAndSetMaterials();
            }
            _targetMeshPair = new KeyValuePair<int, Mesh>(_uniqueMeshId, _targetMesh);
        }
        
        protected virtual void Start()
        {
            GpuInstanceManager = GpuInstanceService.Instance;
            _currentData = new GPUInstanceData(_targetTransform);
            SendInstanceDataToManager();
            _isInitialized = true;
        }

        protected virtual void OnEnable()
        {
            if (!_isInitialized)
                return;
            SendInstanceDataToManager();
        }
        
        protected virtual void OnDisable()
        {
            if (!_isInitialized)
                return;
            RemoveInstanceDataFromManager();
        }
        
        protected virtual void Update()
        {
            if (!_targetTransform.hasChanged)
                return;
            SendInstanceDataToManager();
            _targetTransform.hasChanged = false;
        }
        
        protected abstract void SendInstanceDataToManager();
        protected abstract void RemoveInstanceDataFromManager();
        
        private void AutoFindAndSetMesh()
        {
            _targetTransform.TryGetComponent(out MeshFilter meshFilter);
            if (meshFilter == null)
            {
                Debug.LogError("Mesh filter not found!", gameObject);
                return;
            }
            _targetMesh = meshFilter.sharedMesh;
        }
        
        private void AutoFindAndSetTargetTransform()
        {
            _targetTransform = transform;
        }
        
        private void AutoFindAndSetMaterials()
        {
            _targetTransform.TryGetComponent(out MeshRenderer meshRenderer);
            if (meshRenderer == null)
            {
                Debug.LogError("Mesh renderer not found!", gameObject);
                return;
            }

            meshRenderer.enabled = false;
            _targetMaterials = meshRenderer.sharedMaterials;
        }
    }
}
