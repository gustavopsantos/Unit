using UnityEngine;

namespace Unit.Editor.Framework
{
    public class Label
    {
        private readonly GUIContent _name;
        private readonly GUIContent _abbreviation;
        
        public Label(string name, string abbreviation)
        {
            _name = new GUIContent(name);
            _abbreviation = new GUIContent(abbreviation);
        }

        public GUIContent Get(bool compact)
        {
            return compact ? _abbreviation : _name;
        }
    }
}