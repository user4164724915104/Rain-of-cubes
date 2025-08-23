using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent (typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Color _color;
    private bool _isChangeColor = false;
    public event Action<Cube> CubeFallenDown;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
    }

    private void Start() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnDisable()
    {
        _renderer.material.color = _color;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.transform.rotation = Quaternion.Euler(0,0,0);
        _isChangeColor = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Plane>(out var _) && !_isChangeColor)
        {
            ChangeColor();
            _isChangeColor = true;
            StartCoroutine(ReleaseCubeCutdown());
        }
    }

    private void ChangeColor() => 
        _renderer.material.color = new(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

    private IEnumerator ReleaseCubeCutdown()
    {
        var wait = new WaitForSeconds(UnityEngine.Random.Range(_minDelay, _maxDelay));

        yield return wait;
        CubeFallenDown?.Invoke(this);
    }
}
