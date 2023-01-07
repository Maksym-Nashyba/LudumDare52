using Asteroids.Meshes.Primitives;
using UnityEngine;

namespace Asteroids.Meshes
{
    public class ShellMeshSculptor : LayerMeshSculptor
    {
        protected override Mesh Generate()
        {
            return PrimitiveMeshLoader.GetMesh(Primitive.Sphere);
        }
    }
}