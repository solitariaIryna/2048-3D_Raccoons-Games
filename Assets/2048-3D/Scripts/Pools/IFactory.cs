using UnityEngine;

namespace G2048_3D.Pool
{
    public interface IFactory<T> 
    {
        T CreateNewItem();
    }
}