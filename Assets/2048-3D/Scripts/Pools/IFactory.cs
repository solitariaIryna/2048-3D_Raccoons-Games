using UnityEngine;

namespace G2048_3D.Pool
{
    public interface IFactory
    {
        T CreateObject<T>(T prefab) where T : MonoBehaviour;
    }
}