using System.Linq;
using UnityEngine;

namespace Unit.Editor.Framework
{
    public static class CellFramework
    {
        public static Rect[] GetRects(float spacing, Rect area, params Cell[] cells)
        {
            var spaceConsumedByCellSpacing = (cells.Length - 1) * spacing;
            var spaceConsumedByFixedSizeCells = cells.Where(c => c.IsFixed).Select(c => c.Width).Sum();
            var availableSpaceToFlexibleSizeCells = Mathf.Max(0, area.width - spaceConsumedByCellSpacing - spaceConsumedByFixedSizeCells);
            var flexibleCellSize = availableSpaceToFlexibleSizeCells / cells.Count(c => !c.IsFixed);

            var x = area.x;
            var ans = new Rect[cells.Length];

            for (int i = 0; i < cells.Length; i++)
            {
                var width = cells[i].GetWidth(flexibleCellSize);
                ans[i] = new Rect(x, area.y, width, area.height);
                x += width;
                x += spacing;
            }

            return ans;
        }
    }
}