using G2048_3D.Helpers;
using G2048_3D.Pool;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace G2048_3D.Gameplay.Entities.Cube
{
    public class CubeEntity : MonoBehaviour, IPushable, IPoolable, Pool.IInitializable, IResetable, IReleaseable<CubeEntity>
    {
        [SerializeField] private CubeVisual _visual;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _additionalGravityForce = -5f;
        public NonAllocEvent<CubeEntity> Released { get; private set; } = new NonAllocEvent<CubeEntity>();

        private bool _initialized;

        public Action<CubeEntity> PushEnded;
        public Action<CubeEntity, CubeEntity> CanBeMerged;

        private Coroutine _pushingCoroutine;
        public int Number { get; private set; } = 2;
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void SetData(CubeData data)
        {
            Number = data.Number;
            transform.position = data.Position;
            _visual.SetNumber(Number);
            _visual.SetColor(data.Config.GetColorForNumber(Number));
        }
        public void Initialize()
        {
            if (_initialized)
                return;
        }

        public void EnablePhysics()
        {
            _rigidbody.isKinematic = false;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<CubeEntity>(out CubeEntity cube))
            {
                if (collision.impulse.magnitude > 0.1f && GetInstanceID() > cube.GetInstanceID())
                {
                    PushEnded?.Invoke(this);
                    CanBeMerged?.Invoke(this, cube);
                }
            }
        }
        private void FixedUpdate()
        {
            if (!_rigidbody.isKinematic)
                _rigidbody.AddForce(Vector3.up * _additionalGravityForce, ForceMode.Acceleration);
            
        }

        public void OnSpawned()
        {
            gameObject.SetActive(true);
        }
        public void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        public void ResetData()
        {
            _rigidbody.isKinematic = true;
            transform.eulerAngles = Vector3.zero;
        }

        public void Release()
        {
            Released?.Invoke(this);
        }

        public void Push(float force)
        {
            if (_pushingCoroutine != null)
                StopCoroutine(_pushingCoroutine);

            _pushingCoroutine =  StartCoroutine(Pushing(force));
        }

        private IEnumerator Pushing(float force)
        {
            EnablePhysics();

            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _rigidbody.AddForce(Vector3.forward * force, ForceMode.Impulse);
            yield return new WaitForSeconds(0.25f);
            yield return new WaitUntil(() => _rigidbody.angularVelocity.magnitude < 0.01f);
            PushEnded?.Invoke(this);
        }
    }
}
