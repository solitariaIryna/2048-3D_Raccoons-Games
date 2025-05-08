using G2048_3D.Gameplay.Entities.Cube;
using G2048_3D.Helpers;
using System.Collections;
using UnityEngine;

namespace G2048_3D.Gameplay.Core
{
    public class MergeCollisionHandler
    {
        private const float MERGE_DELAY = 0.3f;
        private const float MiN_IMPULSE = 0.01f;
        private readonly ICoroutineRunner _coroutineRunner;

        private bool _canMerge;

        public bool CanMerge => _canMerge;

        public MergeCollisionHandler(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        public void StartMergeDelay()
        {
            _coroutineRunner.StartCoroutine(EnableMergeAfterDelay());
        }

        private IEnumerator EnableMergeAfterDelay()
        {
            yield return new WaitForSeconds(MERGE_DELAY);
            _canMerge = true;
        }

        public void ResetMergeState()
        {
            _canMerge = false;
        }

        public bool TryHandleCollision(Collision collision, out MergableCube mergableCube)
        {
            mergableCube = null;

            if (!_canMerge)
                return false;
          
            if (collision.impulse.magnitude > MiN_IMPULSE)
            {
                if (collision.collider.TryGetComponent(out mergableCube))
                {
                    return true;
                }
            }
                       
            mergableCube = null;
            return false;
        }
    }
}
