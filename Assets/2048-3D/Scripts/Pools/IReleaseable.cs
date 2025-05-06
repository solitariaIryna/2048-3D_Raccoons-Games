using G2048_3D.Helpers;

namespace G2048_3D.Pool
{
    public interface IReleaseable<T>
    {
        NonAllocEvent<T> Released { get; }

        void Release();
    }
}