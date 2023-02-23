using UnityEngine;

namespace GPUInstanceManager.Demo
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private Cube _cube;
        [SerializeField] private Cube _secondCube;
        [SerializeField] private int _spawnAmount;
        [SerializeField] private Vector2 _xRange;
        [SerializeField] private Vector2 _yRange;
        private void Start()
        {
            for (int i = 0; i < _spawnAmount / 2; i++)
            {
                Instantiate(_cube,
                    new Vector3(Random.Range(_xRange.x, _xRange.y), Random.Range(_yRange.x, _yRange.y), 0),
                    Quaternion.identity);
            }
            
            for (int i = 0; i < _spawnAmount / 2; i++)
            {
                Instantiate(_secondCube,
                    new Vector3(Random.Range(_xRange.x, _xRange.y), Random.Range(_yRange.x, _yRange.y), 0),
                    Quaternion.identity);
            }
        }
    }
}