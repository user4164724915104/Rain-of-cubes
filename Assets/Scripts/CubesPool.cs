using UnityEngine;
using UnityEngine.Pool;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private Plane _plane;
    private ObjectPool<Cube> _pool;

    private void OnEnable()
    {
        _plane.OnCubeHit += Release;
    }

    private void OnDisable()
    {
        _plane.OnCubeHit -= Release;
    }

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

    void Start() => InvokeRepeating(nameof(GetCube), 0, _repeatRate);

    private void GetCube() => _pool.Get();

    private void Release(Cube cube) => _pool.Release(cube);

    private void ActionOnGet(Cube cube)
    {
        float spawnPointX = Random.Range(-6f, 6f);
        float spawnPointZ = Random.Range(-6f, 6f);
        float spawnPointY = 10;
        cube.transform.position = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
        //cube.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        cube.gameObject.SetActive(true);
    }
}
