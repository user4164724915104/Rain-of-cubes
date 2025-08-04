using System;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public event Action<Cube> OnCubeHit;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube))
        {
            OnCubeHit?.Invoke(cube);
        }
    }
}
