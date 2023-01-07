using Asteroids.Meshes.Primitives;
using UnityEngine;

namespace Asteroids.Meshes
{
    public class CoreMeshSculptor : LayerMeshSculptor
    {
        protected override Mesh Generate()
        {
            return PrimitiveMeshLoader.GetMesh(Primitive.Sphere);
        }
    }
}