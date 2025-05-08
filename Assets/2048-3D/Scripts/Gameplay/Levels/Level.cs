using G2048_3D.Gameplay.Core;
using UnityEngine;

namespace G2048_3D.Gameplay.Levels
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform CubeSpawnPoint { get; private set; }
        [field: SerializeField] public TriggerObserver DeadArea { get; private set; }
    }
}
