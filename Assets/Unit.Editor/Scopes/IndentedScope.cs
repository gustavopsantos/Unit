using System;
using UnityEditor;

namespace Unit.Editor.Scopes
{
    public class IndentedScope : IDisposable
    {
        private readonly int _previousValue;

        public IndentedScope(int value)
        {
            _previousValue = EditorGUI.indentLevel;
            EditorGUI.indentLevel = value;
        }

        public void Dispose()
        {
            EditorGUI.indentLevel = _previousValue;
        }
    }
}