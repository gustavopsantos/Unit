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
        private Cell[] _cells;
        
        private int _year;
        private int _month;
        private int _day;
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;
        private DateTimeKind _kind;

        private Label _yearLabel;
        private Label _monthLabel;
        private Label _dayLabel;
        private Label _hourLabel;
        private Label _minuteLabel;
        private Label _secondLabel;
        private Label _millisecondLabel;

        private UDateTime.Precision _precision;

        protected override void Initialize(SerializedProperty property)
        {
            _yearLabel = new Label("Year", "Y");
            _monthLabel = new Label("Month", "M");
            _dayLabel = new Label("Day", "D");
            _hourLabel = new Label("Hour", "H");
            _minuteLabel = new Label("Minute", "M");
            _secondLabel = new Label("Second", "S");
            _millisecondLabel = new Label("Millisecond", "MS");

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

                var compact = usableArea.width < 520;

                EditorGUI.BeginChangeCheck();
                {
                    using (new IndentedScope(0)) // Prevents nested fields to be indented
                    {
                        var rects = CellFramework.GetRects(3, usableArea, _cells);

                        for (int i = 0; i < ((int) _precision) + 1; i++)
                        {
                            DrawField(i, rects[i], dateTime, compact);
                        }
                        
                        _kind = (DateTimeKind) EditorGUI.EnumPopup(rects.Last(), dateTime.Kind);
                    }
                }
                var changed = EditorGUI.EndChangeCheck();

                if (changed && TryCreateValidDateTime(out var dt))
                {
                    property.Set<UDateTime>((UDateTime) dt);
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }
        }

        private void DrawField(int index, Rect rect, DateTime dateTime, bool compact)
        {
            switch (index)
            {
                case 0: _year = GUIUtilities.IntField(_yearLabel.Get(compact), rect, dateTime.Year); break;
                case 1: _month = GUIUtilities.IntField(_monthLabel.Get(compact), rect, dateTime.Month); break;
                case 2: _day = GUIUtilities.IntField(_dayLabel.Get(compact), rect, dateTime.Day); break;
                case 3: _hour = GUIUtilities.IntField(_hourLabel.Get(compact), rect, dateTime.Hour); break;
                case 4: _minute = GUIUtilities.IntField(_minuteLabel.Get(compact), rect, dateTime.Minute); break;
                case 5: _second = GUIUtilities.IntField(_secondLabel.Get(compact), rect, dateTime.Second); break;
                case 6: _millisecond = GUIUtilities.IntField(_millisecondLabel.Get(compact), rect, dateTime.Millisecond); break;
            }
        }

        private bool TryCreateValidDateTime(out DateTime dateTime)
        {
            try
            {
                dateTime = new DateTime(_year, _month, _day, _hour, _minute, _second, _millisecond, _kind);
                return true;
            }
            catch (Exception e)
            {
                dateTime = DateTime.MinValue;
                return false;
            }
        }

        private bool IsValid()
        {
            try
            {
                var dt = new DateTime(_year, _month, _day, _hour, _minute, _second, _millisecond, _kind);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}