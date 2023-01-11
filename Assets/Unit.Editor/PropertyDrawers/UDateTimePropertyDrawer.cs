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


        protected override void Initialize(SerializedProperty property)
        {
            _yearLabel = new Label("Year", "Y");
            _monthLabel = new Label("Month", "M");
            _dayLabel = new Label("Day", "D");
            _hourLabel = new Label("Hour", "H");
            _minuteLabel = new Label("Minute", "M");
            _secondLabel = new Label("Second", "S");
            _millisecondLabel = new Label("Millisecond", "MS");

            _cells = GenerateCells(default);
        }

        private static UDateTime.Precision GetPrecision(SerializedProperty property)
        {
            return property.TryGetAttribute<UDateTime.PrecisionAttribute>(out var precisionAttribute)
                ? precisionAttribute.Precision
                : UDateTime.Precision.Hour;
        }

        private static Cell[] GenerateCells(UDateTime.Precision precision)
        {
            return Enumerable.Range(0, 7).Select(_ => new Cell()).Concat(new Cell(90).Yield()).ToArray();
        }

        protected override void Present(Rect area, SerializedProperty property, GUIContent label)
        {
            var uDateTime = property.As<UDateTime>();
            var dateTime = (DateTime) uDateTime;
            var precision = GetPrecision(property);

            using (new SerializedPropertyScope(area, label, property))
            {
                var usableArea = EditorGUI.PrefixLabel(area, GUIUtility.GetControlID(FocusType.Passive), label);

                var compact = usableArea.width < 520;

                EditorGUI.BeginChangeCheck();
                {
                    using (new IndentedScope(0)) // Prevents nested fields to be indented
                    {
                        var rects = CellFramework.GetRects(3, usableArea, _cells);
                        _year = GUIUtilities.IntField(_yearLabel.Get(compact), rects[0], dateTime.Year);
                        _month = GUIUtilities.IntField(_monthLabel.Get(compact), rects[1], dateTime.Month);
                        _day = GUIUtilities.IntField(_dayLabel.Get(compact), rects[2], dateTime.Day);
                        _hour = GUIUtilities.IntField(_hourLabel.Get(compact), rects[3], dateTime.Hour);
                        _minute = GUIUtilities.IntField(_minuteLabel.Get(compact), rects[4], dateTime.Minute);
                        _second = GUIUtilities.IntField(_secondLabel.Get(compact), rects[5], dateTime.Second);
                        _millisecond = GUIUtilities.IntField(_millisecondLabel.Get(compact), rects[6], dateTime.Second);
                        _kind = (DateTimeKind) EditorGUI.EnumPopup(rects[7], dateTime.Kind);
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