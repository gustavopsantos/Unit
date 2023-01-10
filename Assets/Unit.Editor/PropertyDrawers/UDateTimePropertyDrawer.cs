using System;
using Unit.Editor.Extensions;
using Unit.Editor.Framework;
using Unit.Editor.Scopes;
using Unit.Editor.Utilities;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unit.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(UDateTime))]
    public class UDateTimePropertyDrawer : PropertyDrawer
    {
        private int _year;
        private int _month;
        private int _day;
        private int _hour;
        private int _minute;
        private int _second;
        private DateTimeKind _kind;

        public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
        {
            var uDateTime = property.As<UDateTime>();
            var dateTime = (DateTime) uDateTime;

            using (new PropertyScope(area, label, property))
            {
                var usableArea = EditorGUI.PrefixLabel(area, GUIUtility.GetControlID(FocusType.Passive), label);

                var compact = usableArea.width < 520;

                var yearLabel = compact ? new GUIContent("Y") : new GUIContent("Year");
                var monthLabel = compact ? new GUIContent("M") : new GUIContent("Month");
                var dayLabel = compact ? new GUIContent("D") : new GUIContent("Day");
                var hourLabel = compact ? new GUIContent("H") : new GUIContent("Hour");
                var minuteLabel = compact ? new GUIContent("M") : new GUIContent("Minute");
                var secondLabel = compact ? new GUIContent("S") : new GUIContent("Second");
                
                // var properties = new UnitSerializedProperty[]
                // {
                //     new UnitSerializedProperty("year", "y", property.FindPropertyRelative(nameof(UDateTime.Year)), Compact),
                //     new UnitSerializedProperty("month", "m", property.FindPropertyRelative(nameof(UDateTime.Month)), Compact),
                //     new UnitSerializedProperty("day", "d", property.FindPropertyRelative(nameof(UDateTime.Day)), Compact),
                //     new UnitSerializedProperty("hour", "h", property.FindPropertyRelative(nameof(UDateTime.Hour)), Compact),
                //     new UnitSerializedProperty("minute", "m", property.FindPropertyRelative(nameof(UDateTime.Minute)), Compact),
                //     new UnitSerializedProperty("second", "s", property.FindPropertyRelative(nameof(UDateTime.Second)), Compact),
                //     new UnitSerializedProperty("millisecond", "ms", property.FindPropertyRelative(nameof(UDateTime.Millisecond)), Compact),
                //     new UnitSerializedProperty("ticks", "t", property.FindPropertyRelative(nameof(UDateTime.Ticks)), Compact),
                //     new UnitSerializedProperty("", "", property.FindPropertyRelative(nameof(UDateTime.Kind)), Compact),
                // };

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

                EditorGUI.BeginChangeCheck();
                {
                    using (new IndentedScope(0)) // Prevents nested fields to be indented
                    {
                        var rects = CellFramework.GetRects(3, usableArea, cells);

                        using (new GUIColorScope(Random.Range(0, 2) == 0 ? Color.red : Color.white))
                        {
                            _year = GUIUtilities.IntField(yearLabel, rects[0], dateTime.Year);
                            _month = GUIUtilities.IntField(monthLabel, rects[1], dateTime.Month);
                            _day = GUIUtilities.IntField(dayLabel, rects[2], dateTime.Day);
                            _hour = GUIUtilities.IntField(hourLabel, rects[3], dateTime.Hour);
                            _minute = GUIUtilities.IntField(minuteLabel, rects[4], dateTime.Minute);
                            _second = GUIUtilities.IntField(secondLabel, rects[5], dateTime.Second);
                            _kind = (DateTimeKind) EditorGUI.EnumPopup(rects[6], dateTime.Kind);
                        }
                    }
                }
                var changed = EditorGUI.EndChangeCheck();

                if (changed)
                {
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                    property.Set<UDateTime>(new DateTime(_year, _month, _day, _hour, _minute, _second, 0, _kind));
                }
            }
        }
    }
}