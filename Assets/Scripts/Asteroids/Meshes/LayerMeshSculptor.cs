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
    }
}