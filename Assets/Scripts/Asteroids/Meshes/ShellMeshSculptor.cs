using Asteroids.Meshes.Primitives;
using UnityEngine;

namespace Asteroids.Meshes
{
    public class ShellMeshSculptor : LayerMeshSculptor
    {
        protected override Mesh Generate()
        {
            Mesh mesh = Instantiate(PrimitiveMeshLoader.GetMesh(Primitive.Sphere));

            Vector3[] vertices = mesh.vertices;
            Vector3 offset = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= 1f + (PerlinNoise.Get3DPerlinNoise( vertices[i] + offset, 2f)/6f
                                    +PerlinNoise.Get3DPerlinNoise( vertices[i] + offset, 4f)/4f);
            }

            mesh.vertices = vertices;
            return mesh;
        }
    }
}