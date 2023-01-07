using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Meshes.Primitives
{
    public static class PrimitiveMeshLoader
    {
        private static Dictionary<Primitive, Mesh> _cachedMeshes = new Dictionary<Primitive, Mesh>();

        public static Mesh GetMesh(Primitive primitive)
        {
            if (_cachedMeshes.ContainsKey(primitive)) return _cachedMeshes[primitive];

            Mesh mesh = Load(primitive);
            _cachedMeshes.Add(primitive, mesh);
            return mesh;
        }

        private static Mesh Load(Primitive primitive)
        {
            GameObject primitiveGameObject = Resources.Load($"Primitives/{primitive.ToString()}") as GameObject;
            return primitiveGameObject.GetComponent<MeshFilter>().sharedMesh;
        }
    }
}