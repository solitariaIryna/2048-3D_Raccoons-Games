using System.Collections;
using System.Collections.Generic;

namespace G2048_3D.Pool
{
    public interface IPool<T>
    {
        T GetItem();
    }
}