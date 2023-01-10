using System;
using UnityEngine;

namespace Unit.Editor.Scopes
{
    public class GUIColorScope : IDisposable
    {
        private readonly Color _previousColor;

        public GUIColorScope(Color color)
        {
            _previousColor = GUI.color;
            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = _previousColor;
        }
    }
}