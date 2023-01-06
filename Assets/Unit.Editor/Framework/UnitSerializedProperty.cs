using Unit.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace Unit.Editor.Framework
{
    public class UnitSerializedProperty
    {
        private readonly GUIContent _label;
        private readonly SerializedProperty _serializedProperty;

        public UnitSerializedProperty(string label, SerializedProperty serializedProperty)
        {
            _label = new GUIContent(label);
            _serializedProperty = serializedProperty;
        }

        public void Present(Rect area)
        {
            EditorGUI.PropertyField(area, _serializedProperty, GUIContent.none);
            var labelRect = CalculateCellLabelRect(area, _label, EditorStyles.label);
            EditorGUI.LabelField(labelRect, _label, EditorStyles.label);
        }
        
        private static Rect CalculateCellLabelRect(Rect area, GUIContent label, GUIStyle style)
        {
            return area.HorizontallyTranslated(area.width - style.CalcSize(label).x - 4);
        }
    }
}