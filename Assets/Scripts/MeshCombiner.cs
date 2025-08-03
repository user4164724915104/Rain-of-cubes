using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField] private List<MeshFilter> _sourceMeshFilters;
    [SerializeField] private MeshFilter _targetMeshFilter;

    [ContextMenu("Combine Meshes")]
    private void CombineMeshes()
    {
        CombineInstance[] combine = new CombineInstance[_sourceMeshFilters.Count];

        for (int i = 0; i < _sourceMeshFilters.Count; i++)
        {
            combine[i].mesh = _sourceMeshFilters[i].sharedMesh;
            combine[i].transform = _sourceMeshFilters[i].transform.localToWorldMatrix;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        _targetMeshFilter.mesh = mesh;
    }
}