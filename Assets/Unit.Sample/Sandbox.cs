using System;
using UnityEditor;

namespace Unit.Sample
{
    public static class Sandbox
    {
        [MenuItem("Sandbox/Foo")]
        private static void Start()
        {
            var dt = new DateTime(2023, 1, 9, 15, 72, 0);
        }
    }
}
