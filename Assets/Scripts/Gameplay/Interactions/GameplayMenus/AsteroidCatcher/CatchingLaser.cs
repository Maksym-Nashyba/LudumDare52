using System.Threading.Tasks;
using Asteroids;
using Misc;
using UnityEngine;

namespace Gameplay.Interactions.GameplayMenus.AsteroidCatcher
{
    public class CatchingLaser : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _turnPoint;
        [SerializeField] private Transform _zeroPoint;
        [SerializeField] private Transform _throwPoint;
        [SerializeField] private Transform _hangarPoint;
        private AsyncExecutor _asyncExecutor;

        private void Awake()
        {
            _asyncExecutor = new AsyncExecutor();
        }

        public async Task Catch(Asteroid asteroid)
        {
            ResetLaser();
            await MoveToTurnPoint();
            await TouchAsteroid(asteroid.transform);
            StopAsteroid(asteroid);
            await MoveAsteroidToTurnPoint(asteroid.transform);
            await MoveAsteroidToThrowPoint(asteroid.transform);
            await ThrowAsteroidIn(asteroid.transform);
            UnfreezeAsteroid(asteroid);
            await RetractLaser();
        }

        private Task RetractLaser()
        {
            return _asyncExecutor.EachFrame(0.35f, t =>
            {
                _lineRenderer.SetPosition(1, Vector3.Lerp(_turnPoint.position, _zeroPoint.position, t));
                _lineRenderer.SetPosition(2, Vector3.Lerp(_hangarPoint.position, _zeroPoint.position, t));
            }, EaseFunctions.EaseOutQuart);
        }

        private void UnfreezeAsteroid(Asteroid asteroid)
        {
            Rigidbody rigidbody = asteroid.GetRigidbody();
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }

        private Task ThrowAsteroidIn(Transform asteroidTransform)
        {
            return _asyncExecutor.EachFrame(0.5f, t =>
            {
                Vector3 lerpedPosition = Vector3.Lerp(_throwPoint.position, _hangarPoint.position, t);
                asteroidTransform.position = lerpedPosition;
                _lineRenderer.SetPosition(2, lerpedPosition);
            }, EaseFunctions.EaseOutQuart);
        }
        
        private Task MoveAsteroidToThrowPoint(Transform asteroidTransform)
        {
            return _asyncExecutor.EachFrame(0.5f, t =>
            {
                Vector3 lerpedPosition = Vector3.Lerp(_turnPoint.position, _throwPoint.position, t);
                asteroidTransform.position = lerpedPosition;
                _lineRenderer.SetPosition(2, lerpedPosition);
            }, EaseFunctions.EaseInCirc);
        }
        
        private Task MoveAsteroidToTurnPoint(Transform asteroidTransform)
        {
            return _asyncExecutor.EachFrame(1.25f, t =>
            {
                Vector3 lerpedPosition = Vector3.Lerp(asteroidTransform.position, _turnPoint.position, t);
                asteroidTransform.position = lerpedPosition;
                _lineRenderer.SetPosition(2, lerpedPosition);
            }, EaseFunctions.EaseOutQuart);
        }
        
        private void StopAsteroid(Asteroid asteroid)
        {
            Rigidbody rigidbody = asteroid.GetRigidbody();
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector3.zero;
        }
        
        private Task TouchAsteroid(Transform asteroidTransform)
        {
            return _asyncExecutor.EachFrame(1.25f, t =>
            {
                Vector3 lerpedPosition = Vector3.Lerp(_turnPoint.position, asteroidTransform.position, t);
                _lineRenderer.SetPosition(2, lerpedPosition);
            }, EaseFunctions.InOutQuad);
        }
        
        private Task MoveToTurnPoint()
        {
            return _asyncExecutor.EachFrame(1.25f, t =>
            {
                Vector3 lerpedPosition = Vector3.Lerp(_zeroPoint.position, _turnPoint.position, t);
                _lineRenderer.SetPosition(1, lerpedPosition);
                _lineRenderer.SetPosition(2, lerpedPosition);
            }, EaseFunctions.EaseOutQuart);
        }
        
        private void ResetLaser()
        {
            Vector3 position = _zeroPoint.position;
            Vector3[] positions = { position, position, position };
            _lineRenderer.SetPositions(positions);
        }
    }
}