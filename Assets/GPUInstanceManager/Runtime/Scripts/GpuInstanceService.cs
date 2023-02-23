using GPUInstanceManager.Interface;
using GPUInstanceManager.Manager.Base;
using UnityEngine;

namespace GPUInstanceManager
{
    public class GpuInstanceService : MonoBehaviour
    {
        public static IGPUInstanceManager Instance { get; private set; }
        [SerializeField] private GpuInstanceManagerBase _gpuInstanceManager;

        private void Awake()
        {
            Instance = _gpuInstanceManager;
        }
    }
}