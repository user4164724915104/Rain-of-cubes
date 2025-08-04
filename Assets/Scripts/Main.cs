using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private Plane _plane;
    [SerializeField] private Cube _cube;
    [SerializeField] private CubesPool _pool;

    private void OnEnable()
    {
        _plane.OnCubeHit += ReleaseCube;
    }

    private void OnDisable()
    {
        _plane.OnCubeHit -= ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        if (cube.IsChangeColor == false)
        {
            _pool.ReleasingCube(_cube.InitializeReleasing(cube));
        }
    }
}
