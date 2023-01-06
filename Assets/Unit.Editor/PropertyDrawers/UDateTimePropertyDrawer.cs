using Unit.Editor.Framework;
using Unit.Editor.Scopes;
using Unit.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Unit.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(UDateTime))]
    public class UDateTimePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
        {
            using (new PropertyScope(area, label, property))
            {
                var usableArea = EditorGUI.PrefixLabel(area, GUIUtility.GetControlID(FocusType.Passive), label);
                
                var labelCompactingThreshold = 800;
                var compact = area.width <= labelCompactingThreshold;

                var yearProperty = property.FindPropertyRelative(nameof(UDateTime.Year));
                var monthProperty = property.FindPropertyRelative(nameof(UDateTime.Month));
                var dayProperty = property.FindPropertyRelative(nameof(UDateTime.Day));
                var hourProperty = property.FindPropertyRelative(nameof(UDateTime.Hour));
                var minuteProperty = property.FindPropertyRelative(nameof(UDateTime.Minute));
                var secondProperty = property.FindPropertyRelative(nameof(UDateTime.Second));
                var kindProperty = property.FindPropertyRelative(nameof(UDateTime.Kind));
            
                var yearLabel = compact ? "y" : "year";
                var monthLabel = compact ? "m" : "month";
                var dayLabel = compact ? "d" : "day";
                var hourLabel = compact ? "h" : "hour";
                var minuteLabel = compact ? "m" : "minute";
                var secondLabel = compact ? "s" : "second";

                var cells = new Cell[]
                {
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(88),
                };
                
                var rects = CellFramework.GetRects(3, usableArea, cells);
                
                using (new IndentedScope(0)) // Prevents nested fields to be indented
                {
                    GUIUtilities.InnerLabeledProperty(new GUIContent(yearLabel), rects[0], yearProperty);
                    GUIUtilities.InnerLabeledProperty(new GUIContent(monthLabel), rects[1], monthProperty);
                    GUIUtilities.InnerLabeledProperty(new GUIContent(dayLabel), rects[2], dayProperty);
                    GUIUtilities.InnerLabeledProperty(new GUIContent(hourLabel), rects[3], hourProperty);
                    GUIUtilities.InnerLabeledProperty(new GUIContent(minuteLabel), rects[4], minuteProperty);
                    GUIUtilities.InnerLabeledProperty(new GUIContent(secondLabel), rects[5], secondProperty);
                    GUIUtilities.InnerLabeledProperty(GUIContent.none, rects[6], kindProperty);
                }
            }
        }
    }
}