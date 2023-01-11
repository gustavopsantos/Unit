using UnityEditor;
using UnityEngine;

namespace Unit.Editor.Framework
{
    public abstract class UnitPropertyDrawer : PropertyDrawer
    {
        private bool _initialized;

        protected abstract void Initialize(SerializedProperty property);
        protected abstract void Present(Rect position, SerializedProperty property, GUIContent label);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!_initialized)
            {
                Initialize(property);
                _initialized = true;
            }
            else
            {
                Present(position, property, label);
            }
        }
    }
}