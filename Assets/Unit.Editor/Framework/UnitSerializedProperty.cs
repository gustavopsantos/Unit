using System;
using Unit.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace Unit.Editor.Framework
{
    public class UnitSerializedProperty
    {
        private readonly GUIContent _label;
        private readonly GUIContent _abbreviation;
        private readonly SerializedProperty _serializedProperty;
        private readonly Func<bool> _compact;

        public UnitSerializedProperty(string label, SerializedProperty serializedProperty, Func<bool> compact)
        {
            _label = new GUIContent(label);
            _abbreviation = string.IsNullOrEmpty(label) ? GUIContent.none : new GUIContent(label[0].ToString());
            _serializedProperty = serializedProperty;
            _compact = compact;
        }

        public void Present(Rect area)
        {
            PresentEditionField(area);
            var label = _compact.Invoke() ? _abbreviation : _label;
            PresentLabelOverlay(label, area);
        }

        private void PresentEditionField(Rect area)
        {
            EditorGUI.PropertyField(area, _serializedProperty, GUIContent.none);
        }

        private static void PresentLabelOverlay(GUIContent label, Rect area)
        {
            var style = EditorStyles.label;
            var rect = CalculateCellLabelRect(area, label, style);
            EditorGUI.LabelField(rect, label, style);
        }
        
        private static Rect CalculateCellLabelRect(Rect area, GUIContent label, GUIStyle style)
        {
            return area.HorizontallyTranslated(area.width - style.CalcSize(label).x - 4);
        }
    }
}