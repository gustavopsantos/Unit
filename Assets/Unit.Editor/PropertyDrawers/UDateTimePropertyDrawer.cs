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

                bool Compact()
                {
                    return usableArea.width < 500;
                }

                var properties = new UnitSerializedProperty[]
                {
                    new UnitSerializedProperty("year", property.FindPropertyRelative(nameof(UDateTime.Year)), Compact),
                    new UnitSerializedProperty("month", property.FindPropertyRelative(nameof(UDateTime.Month)), Compact),
                    new UnitSerializedProperty("day", property.FindPropertyRelative(nameof(UDateTime.Day)), Compact),
                    new UnitSerializedProperty("hour", property.FindPropertyRelative(nameof(UDateTime.Hour)), Compact),
                    new UnitSerializedProperty("minute", property.FindPropertyRelative(nameof(UDateTime.Minute)), Compact),
                    new UnitSerializedProperty("second", property.FindPropertyRelative(nameof(UDateTime.Second)), Compact),
                    new UnitSerializedProperty(string.Empty, property.FindPropertyRelative(nameof(UDateTime.Kind)), Compact),
                };

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

                using (new IndentedScope(0)) // Prevents nested fields to be indented
                {
                    var rects = CellFramework.GetRects(3, usableArea, cells);

                    for (var i = 0; i < properties.Length; i++)
                    {
                        properties[i].Present(rects[i]);
                    }
                }
            }
        }
    }
}