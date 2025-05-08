using System;
using UnityEngine;

namespace G2048_3D.Gameplay.Core
{
    public class TriggerObserver : MonoBehaviour
    {
        public Action<Collider> TriggerEnter;
        private void OnTriggerEnter(Collider other) => 
            TriggerEnter?.Invoke(other);
    }
}
