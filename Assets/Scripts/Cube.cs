using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool IsChangeColor { get => _isChangeColor; }
    private bool _isChangeColor = false;

    public void Initialize(bool isChangeColor, Color color)
    {
        _isChangeColor = isChangeColor;
        GetComponent<Renderer>().material.color = color;
    }
}
