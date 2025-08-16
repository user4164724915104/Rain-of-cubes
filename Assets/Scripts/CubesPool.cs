using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public struct SpawnAreaRange
{
    public float Min;
    public float Max;
    public float Height;
}

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private SpawnAreaRange _spawnAreaRange = new();
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => InstantiateCube(),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease: (cube) => cube.gameObject.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start() =>
        StartCoroutine(GetCubeCutdown(_repeatRate));

    private void GetCube() =>
        _pool.Get();
    
    private Cube InstantiateCube()
    {
        return Instantiate(_cubePrefab);
    }

    private void ReleaseCube(Cube cube)
    {
        cube.CubeFallenDown -= ReleaseCube;
        _pool.Release(cube);
    }

    private void ActionOnGet(Cube cube)
    {
        float spawnPointX = Random.Range(_spawnAreaRange.Min,_spawnAreaRange.Max);
        float spawnPointZ = Random.Range(_spawnAreaRange.Min, _spawnAreaRange.Max);
        cube.transform.position = new Vector3(spawnPointX, _spawnAreaRange.Height, spawnPointZ);
        cube.gameObject.SetActive(true);
        cube.CubeFallenDown += ReleaseCube;
    }

    private IEnumerator GetCubeCutdown(float repeatRate)
    {
        WaitForSeconds wait = new(repeatRate);

        while (enabled)
        {
            GetCube();

            yield return wait;
        }
    }
}
