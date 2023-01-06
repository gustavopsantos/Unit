namespace Unit.Editor.Framework
{
    public class Cell
    {
        public float Width { get; }

        public bool IsFixed => Width != -1f;

        public Cell() : this(-1f)
        {
        }

        public Cell(float width)
        {
            Width = width;
        }

        public float GetWidth(float width)
        {
            return IsFixed ? Width : width;
        }
    }
}