using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace G2048_3D.Pool
{
    public interface IPool<T>
    {
        T GetItem();
    }
}