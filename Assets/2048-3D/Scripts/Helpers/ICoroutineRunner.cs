using System.Collections;
using UnityEngine;

namespace G2048_3D.Helpers
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
        void StopCoroutine(Coroutine coroutine);
    }
}
