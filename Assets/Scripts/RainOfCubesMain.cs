using UnityEngine;

public class RainOfCubesMain : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private CubesPool _pool;

    private void OnEnable()
    {
        _cube.CubeFallenDown += ReleaseCube;
    }

    private void OnDisable()
    {
        _cube.CubeFallenDown -= ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        Debug.Log(cube.ToString());
        _pool.ReleasingCube(cube);
    }
}
