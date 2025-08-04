using UnityEngine;

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private bool _isChangeColor = false;

    public bool IsChangeColor { get => _isChangeColor; }

    private void Start() => _renderer = GetComponent<Renderer>();

    private void OnDisable()
    {
        _renderer.material.color = Color.gray;
        _isChangeColor = false;
    }

    public Cube InitializeReleasing(Cube cube)
    {
        cube._renderer.material.color = Random.ColorHSV();
        cube._isChangeColor = true;
        return cube;
    }
}
