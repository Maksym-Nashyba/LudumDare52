using System.Threading;
using System.Threading.Tasks;
using Asteroids;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactions.Tools
{
    public class PlacedBomb : MonoBehaviour
    {
        [SerializeField] private UnityEvent _ticked;
        [SerializeField] private UnityEvent _exploded;
        private Asteroid _asteroid;
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public void AttachToAsteroid(Asteroid asteroid, Vector3 position)
        {
            _asteroid = asteroid;
            transform.position = position;
            transform.LookAt(transform.position + (transform.position - asteroid.transform.position));
            transform.SetParent(asteroid.transform);
            Charge(2.5f, _cancellationTokenSource.Token);
        }

        private async Task Charge(float timeSeconds, CancellationToken cancellationToken)
        {
            for (int i = 0; i < 3; i++)
            {
                _ticked?.Invoke();
                await Task.Delay((int)(timeSeconds/3 * 1000));
                if(cancellationToken.IsCancellationRequested)return;
            }
            Explode();
        }

        private void Explode()
        {
            while (_asteroid != null && !_asteroid.IsDestroyed)
            {
                _asteroid.DestroyOuterLayer();
            }

            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.enabled = false;
            }
            _exploded?.Invoke();
            Destroy(this);
        }
    }
}