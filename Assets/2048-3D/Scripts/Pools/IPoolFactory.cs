namespace G2048_3D.Pool
{
    public interface IPoolFactory<T>
    {
        T CreateNewItem();
    }
}