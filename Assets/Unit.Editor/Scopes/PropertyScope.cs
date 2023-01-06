using System;
using UnityEditor;
using UnityEngine;

namespace Unit.Editor.Scopes
{
    /// <summary>
    /// Using Begin/EndProperty on the parent property means that prefab override logic works on the entire property.
    /// </summary>
    public class PropertyScope : IDisposable
    {
        public PropertyScope(Rect position, GUIContent label, SerializedProperty property)
        {
            EditorGUI.BeginProperty(position, label, property);
        }
        
        public void Dispose()
        {
            EditorGUI.EndProperty();
        }
    }
}