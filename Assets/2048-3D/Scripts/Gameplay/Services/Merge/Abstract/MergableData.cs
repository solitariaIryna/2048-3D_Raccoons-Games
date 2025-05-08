namespace G2048_3D.Gameplay.Services
{
    public class MergableData<TData, TVisual>
    {
        public TData First;
        public TData Second;

        public TVisual VisualFirst;
        public TVisual VisualSecond;

        public MergableData(TData first, TData second, TVisual visualFirst, TVisual visualSecond)
        {
            First = first;
            Second = second;
            VisualFirst = visualFirst;
            VisualSecond = visualSecond;
        }
    }
}
