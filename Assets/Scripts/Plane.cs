using System;
using System.Collections;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public event Action<Cube> OnCubeHit;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube))
        {
            StartCoroutine(TimeToDestroy(cube));
        }
    }
    private IEnumerator TimeToDestroy(Cube cube)
    {
        if (!cube.IsChangeColor)
        {
            cube.Initialize(true, UnityEngine.Random.ColorHSV());
            yield return new WaitForSeconds(UnityEngine.Random.Range(2,5));
            OnCubeHit?.Invoke(cube);
            cube.Initialize(false, Color.gray);
        }
    }
}
