using G2048_3D.Helpers;
using System;
using System.Collections;
using UnityEngine;

namespace G2048_3D.Gameplay.Core
{
    [Serializable]
    public class DirectionMovementPusher : IPushable
    {
        [SerializeField] private Rigidbody _rigidbody;

        private Coroutine _pushingCoroutine;
        private ICoroutineRunner _coroutineRunner;

        public event Action Pushed;
        public event Action PushCompleted;

        public Vector3 Position
        {
            get => _rigidbody.transform.position;
            set => _rigidbody.transform.position = value;
        }
        public void Construct(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        public void EnablePhysics(bool value)
        {
            _rigidbody.isKinematic = value;
        }
        public void ResetPhysics()
        {
            EnablePhysics(false);
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.linearVelocity = Vector3.zero;

        }
        public void Push(Vector3 force)
        {
            if (_pushingCoroutine != null)
                _coroutineRunner.StopCoroutine(_pushingCoroutine);

            Pushed?.Invoke();
            _pushingCoroutine = _coroutineRunner.StartCoroutine(Pushing(force));
        }
        private IEnumerator Pushing(Vector3 force)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(force, ForceMode.Impulse);

            yield return new WaitForSeconds(0.25f);
            yield return new WaitUntil(() => _rigidbody.angularVelocity.magnitude < 0.01f);
            PushCompleted?.Invoke();
        }
    }
}
