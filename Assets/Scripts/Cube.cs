using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private bool _isChangeColor = false;
    public event Action<Cube> CubeFallenDown;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        _renderer.material.color = Color.gray;
        _rigidbody.linearVelocity = Vector3.zero;
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
