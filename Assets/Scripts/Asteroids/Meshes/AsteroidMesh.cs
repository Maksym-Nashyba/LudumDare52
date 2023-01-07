using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Meshes
{
    public class AsteroidMesh : MonoBehaviour
    {
        private List<LayerMeshSculptor> _sculptors;

        public void BuildMeshes(Queue<AsteroidLayer> layers)
        {
            _sculptors = new List<LayerMeshSculptor>(layers.Count);
            while (layers.Count > 1)
            {
                InstantiateSculptor<ShellMeshSculptor>(layers.Dequeue());
            }
            InstantiateSculptor<CoreMeshSculptor>(layers.Peek());
        }
        
        private void InstantiateSculptor<T>(AsteroidLayer layer) where T : LayerMeshSculptor
        {
            LayerMeshSculptor sculptor = layer.gameObject.AddComponent<T>();
            sculptor.Build();
            _sculptors.Add(sculptor);
        }
    }
}