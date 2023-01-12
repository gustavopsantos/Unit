using Unit.Editor.Extensions;
using UnityEditor;
using UnityEngine;

namespace Unit.Editor.Utilities
{
    internal static class GUIUtilities
    {
        public static int IntField(GUIContent label, Rect area, int value)
        {
            var labelStyle = new GUIStyle(EditorStyles.label);
            labelStyle.normal.textColor = Color.gray;
            var labelRect = CalculateCellLabelRect(area, label, labelStyle);
            var newValue = EditorGUI.IntField(area, GUIContent.none, value);
            EditorGUI.LabelField(labelRect, label, labelStyle);
            return newValue;
        }
        
        private static Rect CalculateCellLabelRect(Rect area, GUIContent label, GUIStyle style)
        {
            return area.HorizontallyTranslated(area.width - style.CalcSize(label).x - 4);
        }
    }
}