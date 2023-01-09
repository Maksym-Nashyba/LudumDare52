using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids;
using Asteroids.Chunks;
using Gameplay.Interactions.GameplayMenus.UpgradeShop;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactions.Tools
{
    public class Vacuum : Tool
    {
        [SerializeField] private UpgradeStation _upgrades;
        [SerializeField] private Interactor _interactor;
        [SerializeField] private SphereCollider _suckZone;
        [SerializeField] private SphereCollider _consumeZone;
        [SerializeField] private int _containerSize;
        [SerializeField] private UnityEvent _sucked;
        [SerializeField] private UnityEvent _spit;
        public List<Chunk> _container { get; private set; }
        private float _cooldownSeconds;
        private Collider[] _colliderBuffer;

        protected override void Awake()
        {
            base.Awake();
            _container = new List<Chunk>(_containerSize);
            _suckZone.enabled = false;
            _colliderBuffer = new Collider[50];
            _upgrades.VacuumUpgrade.Crafted += OnVacuumUpgraded;
        }

        private void Update()
        {
            _cooldownSeconds = Mathf.Clamp(_cooldownSeconds - Time.deltaTime, 0f, float.MaxValue);
            if(_interactor.IsLocked)return;
            if (Input.GetMouseButton(0)) Suck();
            else if(Input.GetMouseButton(1)) Blow();
        }

        private void OnVacuumUpgraded()
        {
            _upgrades.VacuumUpgrade.Crafted -= OnVacuumUpgraded;
            _containerSize *= 2;
            _container.Capacity = _containerSize;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!Input.GetMouseButton(0)) return;
            if (other.TryGetComponent(out Chunk chunk))
            {
                Consume(chunk);
            }
        }

        private void Consume(Chunk chunk)
        {
            if(_container.Count >= _container.Capacity || InCD) return;
            chunk.gameObject.SetActive(false);
            _container.Add(chunk);
            _sucked?.Invoke();
        }

        private bool InCD => _cooldownSeconds > 0.005f;
        
        private void Suck()
        {
            if(_container.Count >= _container.Capacity || InCD) return;
            Vector3 target = _consumeZone.transform.position;
            Physics.OverlapSphereNonAlloc(_suckZone.transform.position, _suckZone.radius, _colliderBuffer, LayerMask.NameToLayer("Draggable"));
            foreach (Collider collider in _colliderBuffer)
            {
                if(collider == null || collider.attachedRigidbody == null) continue;
                if(!collider.attachedRigidbody.TryGetComponent(out Chunk chunk))continue;
                Rigidbody rigidbody = chunk.GetRigidbody();
                rigidbody.velocity = Vector3.zero;
                float distance = (target - rigidbody.position).magnitude;
                float distance01 = Mathf.Clamp01(distance / 4f);
                rigidbody.MovePosition(Vector3.Lerp(rigidbody.position, target, Time.deltaTime * (1f-distance01) * 3f));
            }
        }

        private void Blow()
        {
            if(_container.Count <= 0 || InCD) return;

            Chunk chunk = _container.First();
            _container.Remove(chunk);
            chunk.gameObject.SetActive(true);
            chunk.transform.position = _consumeZone.transform.position;
            chunk.GetRigidbody().AddForce(transform.forward * 100f, ForceMode.VelocityChange);
            _spit?.Invoke();
            
            _cooldownSeconds += 0.10f;
        }
        
        public override void Apply(Asteroid asteroid, Vector3 position)
        {
        }
    }
}