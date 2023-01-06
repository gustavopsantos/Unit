using Unit.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace Unit.Editor.Utilities
{
    internal static class GUIUtilities
    {
        public static void InnerLabeledProperty(GUIContent label, Rect area, SerializedProperty property)
        {
            var labelStyle = EditorStyles.label;
            var labelRect = CalculateCellLabelRect(area, label, labelStyle);
            EditorGUI.PropertyField(area, property, GUIContent.none);
            EditorGUI.LabelField(labelRect, label, labelStyle);
        }

        private static Rect CalculateCellLabelRect(Rect area, GUIContent label, GUIStyle style)
        {
            return area.HorizontallyTranslated(area.width - style.CalcSize(label).x - 4);
        }
    }
}