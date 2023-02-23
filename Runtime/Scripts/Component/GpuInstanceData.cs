using UnityEngine;

namespace GPUInstanceManager.Component
{
    public class GPUInstanceData
    {
        public GPUInstanceData(Transform transform)
        {
            this._position = transform.position;
            this._rotation = transform.rotation;
            this._scale = transform.lossyScale;
            UpdateMatrix();
        }

        private Matrix4x4 GetMatrix()
        {
            return Matrix4x4.TRS(this._position, this._rotation, this._scale);
        }
        
        public Matrix4x4 Matrix { get; private set; }
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;

        public void SetPosition(Vector3 position)
        {
            this._position = position;
        }
        
        public void SetRotation(Quaternion rotation)
        {
            this._rotation = rotation;
        }
        
        public void SetScale(Vector3 scale)
        {
            this._scale = scale;
        }

        public void UpdateMatrix()
        {
            this.Matrix = GetMatrix();
        }
    }
}