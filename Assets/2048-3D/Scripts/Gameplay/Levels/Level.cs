
using UnityEngine;

namespace G2048_3D.Gameplay.Levels
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform CubeSpawnPoint { get; private set; }
    }
}
