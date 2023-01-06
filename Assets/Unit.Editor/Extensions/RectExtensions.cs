using UnityEngine;

namespace Unit.Editor.Extensions
{
    internal static class RectExtensions
    {
        internal static Rect VerticallyTranslated(this Rect rect, float displacement)
        {
            rect.y += displacement;
            return rect;
        }
        
        internal static Rect HorizontallyTranslated(this Rect rect, float displacement)
        {
            rect.x += displacement;
            return rect;
        }
        
        internal static Rect SetHorizontalPosition(this Rect rect, float value)
        {
            rect.x = value;
            return rect;
        }
        
        internal static Rect WithWidth(this Rect rect, float value)
        {
            rect.width = value;
            return rect;
        }
        
        internal static Rect WithHeight(this Rect rect, float value)
        {
            rect.height = value;
            return rect;
        }
    }
}