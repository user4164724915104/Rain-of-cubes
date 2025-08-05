using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
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

    private void ReleaseCube(Cube cube) =>
        _pool.Release(cube);

    private void ActionOnGet(Cube cube)
    {
        float spawnPointX = Random.Range(-6f, 6f);
        float spawnPointZ = Random.Range(-6f, 6f);
        float spawnPointY = 10;
        cube.transform.position = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
        cube.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        cube.gameObject.SetActive(true);
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

    public void ReleasingCube(Cube cube)
    {
        if (cube != null)
        {
            ReleaseCube(cube);
        }
    }
}
