using Unit.Editor.Framework;
using Unit.Editor.Scopes;
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
                
                var yearProperty = new UnitSerializedProperty(
                    "year", property.FindPropertyRelative(nameof(UDateTime.Year)));
                
                var monthProperty = new UnitSerializedProperty(
                    "month", property.FindPropertyRelative(nameof(UDateTime.Month)));
                
                var dayProperty = new UnitSerializedProperty(
                    "day", property.FindPropertyRelative(nameof(UDateTime.Day)));
                
                var hourProperty = new UnitSerializedProperty(
                    "hour", property.FindPropertyRelative(nameof(UDateTime.Hour)));
                
                var minuteProperty = new UnitSerializedProperty(
                    "minute", property.FindPropertyRelative(nameof(UDateTime.Minute)));
                
                var secondProperty = new UnitSerializedProperty(
                    "second", property.FindPropertyRelative(nameof(UDateTime.Second)));
                
                var kindProperty = new UnitSerializedProperty(
                    string.Empty, property.FindPropertyRelative(nameof(UDateTime.Kind)));

                var cells = new Cell[]
                {
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(),
                    new Cell(90),
                };
                
                var rects = CellFramework.GetRects(3, usableArea, cells);
                
                using (new IndentedScope(0)) // Prevents nested fields to be indented
                {
                    yearProperty.Present(rects[0]);
                    monthProperty.Present(rects[1]);
                    dayProperty.Present(rects[2]);
                    hourProperty.Present(rects[3]);
                    minuteProperty.Present(rects[4]);
                    secondProperty.Present(rects[5]);
                    kindProperty.Present(rects[6]);
                }
            }
        }
    }
}