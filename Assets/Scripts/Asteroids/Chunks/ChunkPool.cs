using Misc;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Chunks
{
    public class ChunkPool : MonoBehaviour
    {
        [SerializeField] private GameObject _chunkPrefab;
        [SerializeField] private AsteroidMaterial _trashMaterial;
        private ObjectPool<Chunk> _chuckPool;

        private void Awake()
        {
            _chuckPool = new ObjectPool<Chunk>(CreateChunkObject, OnChunkGet, OnChunkRelease, OnChunkDestroy, 
                true, 120, 300);
        }

        public void SpawnChunks(Vector3 position, int medianAmount, AsteroidMaterial material, float richness)
        {
            int amount = medianAmount + Random.Range(-medianAmount / 2, 1 + medianAmount / 2);
            for (int i = 0; i < amount; i++)
            {
                material = Random.Range(0f, 1f) < richness ? material : _trashMaterial;
                position += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f));
                SpawnChunk(material, position);
            }
        }

        public void ReturnChunk(Chunk chunk)
        {
            _chuckPool.Release(chunk);
        }

        private void SpawnChunk(AsteroidMaterial material, Vector3 position)
        {
            Chunk chunk = _chuckPool.Get();
            chunk.ApplyMaterial(material);
            chunk.transform.position = position;
        }

        private Chunk CreateChunkObject()
        {
            return Instantiate(_chunkPrefab).GetComponent<Chunk>();
        }

        private void OnChunkGet(Chunk chunk)
        {
            chunk.gameObject.SetActive(true);
        }

        private void OnChunkRelease(Chunk chunk)
        {
            chunk.gameObject.SetActive(false);
        }

        private void OnChunkDestroy(Chunk chunk)
        {
            Destroy(chunk.gameObject);
        }
    }
}