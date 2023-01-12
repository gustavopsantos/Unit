using System;
using System.Linq;
using Unit.Editor.Extensions;
using Unit.Editor.Framework;
using Unit.Editor.Scopes;
using Unit.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Unit.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(UDateTime))]
    public class UDateTimePropertyDrawer : UnitPropertyDrawer
    {
        private int _year;
        private int _month;
        private int _day;
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;
        private DateTimeKind _kind;

        private Cell[] _cells;
        private GUIContent _yearLabel;
        private GUIContent _monthLabel;
        private GUIContent _dayLabel;
        private GUIContent _hourLabel;
        private GUIContent _minuteLabel;
        private GUIContent _secondLabel;
        private GUIContent _millisecondLabel;
        private UDateTime.Precision _precision;

        protected override void Initialize(SerializedProperty property)
        {
            _yearLabel = new GUIContent("Y");
            _monthLabel = new GUIContent("M");
            _dayLabel = new GUIContent("D");
            _hourLabel = new GUIContent("H");
            _minuteLabel = new GUIContent("M");
            _secondLabel = new GUIContent("S");
            _millisecondLabel = new GUIContent("MS");

            _precision = GetPrecision(property);
            _cells = GenerateCells(_precision);
        }

        private static UDateTime.Precision GetPrecision(SerializedProperty property)
        {
            return property.TryGetAttribute<UDateTime.PrecisionAttribute>(out var precisionAttribute)
                ? precisionAttribute.Precision
                : UDateTime.Precision.Hour;
        }

        private static Cell[] GenerateCells(UDateTime.Precision precision)
        {
            return Enumerable
                .Range(0, ((int) precision) + 1)
                .Select(_ => new Cell())
                .Concat(new Cell(90).Yield())
                .ToArray();
        }

        protected override void Present(Rect area, SerializedProperty property, GUIContent label)
        {
            var uDateTime = property.As<UDateTime>();
            var dateTime = (DateTime) uDateTime;

            using (new SerializedPropertyScope(area, label, property))
            {
                var usableArea = EditorGUI.PrefixLabel(area, GUIUtility.GetControlID(FocusType.Passive), label);

                EditorGUI.BeginChangeCheck();
                {
                    using (new IndentedScope(0)) // Prevents nested fields to be indented
                    {
                        var rects = CellFramework.GetRects(3, usableArea, _cells);

                        for (int i = 0; i < ((int) _precision) + 1; i++)
                        {
                            DrawField(i, rects[i], dateTime);
                        }
                        
                        _kind = (DateTimeKind) EditorGUI.EnumPopup(rects.Last(), dateTime.Kind);
                    }
                }
                var changed = EditorGUI.EndChangeCheck();

                if (changed && TryCreateValidDateTime(out var dt))
                {
                    Undo.RecordObject(property.serializedObject.targetObject, string.Empty);
                    Undo.SetCurrentGroupName($"set UDateTime to ({dt:O}) in {property.serializedObject.targetObject.name}");
                    property.Set<UDateTime>((UDateTime) dt);
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }
        }

        private void DrawField(int index, Rect rect, DateTime dateTime)
        {
            switch (index)
            {
                case 0: _year = GUIUtilities.IntField(_yearLabel, rect, dateTime.Year); break;
                case 1: _month = GUIUtilities.IntField(_monthLabel, rect, dateTime.Month); break;
                case 2: _day = GUIUtilities.IntField(_dayLabel, rect, dateTime.Day); break;
                case 3: _hour = GUIUtilities.IntField(_hourLabel, rect, dateTime.Hour); break;
                case 4: _minute = GUIUtilities.IntField(_minuteLabel, rect, dateTime.Minute); break;
                case 5: _second = GUIUtilities.IntField(_secondLabel, rect, dateTime.Second); break;
                case 6: _millisecond = GUIUtilities.IntField(_millisecondLabel, rect, dateTime.Millisecond); break;
            }
        }

        private bool TryCreateValidDateTime(out DateTime dateTime)
        {
            try
            {
                dateTime = new DateTime(_year, _month, _day, _hour, _minute, _second, _millisecond, _kind);
                return true;
            }
            catch (Exception)
            {
                dateTime = DateTime.MinValue;
                return false;
            }
        }
    }
}