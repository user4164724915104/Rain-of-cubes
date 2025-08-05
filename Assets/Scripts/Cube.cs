using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    private Renderer _renderer;
    private bool _isChangeColor = false;
    public event Action<Cube> CubeFallenDown;

    public bool IsChangeColor { get => _isChangeColor; }

    private void Start() =>
        _renderer = GetComponent<Renderer>();

    private void OnDisable()
    {
        _renderer.material.color = Color.gray;
        _isChangeColor = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Plane>(out var i) && !_isChangeColor)
        {
            _renderer.material.color = UnityEngine.Random.ColorHSV();
            _isChangeColor = true;
            StartCoroutine(ReleaseCubeCutdown());
        }
    }

    private IEnumerator ReleaseCubeCutdown()
    {
        var wait = new WaitForSeconds(UnityEngine.Random.Range(_minDelay, _maxDelay));

        yield return wait;
        CubeFallenDown?.Invoke(this);
    }
}
