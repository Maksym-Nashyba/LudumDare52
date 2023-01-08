using System;
using UnityEngine;

namespace Asteroids.Meshes
{
    public abstract class LayerMeshSculptor : MonoBehaviour
    {
        public event Action<Mesh> Changed;
        public Mesh Mesh { get; private set; }
        
        public void Build()
        {
            Mesh = Generate();
            Mesh.MarkDynamic();
            Mesh.MarkModified();
        }
        
        protected abstract Mesh Generate();

        protected void Apply()
        {
            Mesh.MarkModified();
            Changed?.Invoke(Mesh);
        }

        public void CarveHole(float radius, float strength, Vector3 localPosition)
        {
            Vector3[] vertices = Mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                float distanceSquared = (vertices[i] - localPosition).sqrMagnitude;
                if (distanceSquared > radius * radius) continue;
                vertices[i] *= 1f-strength;
            }

            Mesh.vertices = vertices;
            Apply();
        }
    }
}