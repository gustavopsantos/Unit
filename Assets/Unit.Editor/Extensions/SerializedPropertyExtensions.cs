using System.Reflection;
using UnityEditor;

namespace Unit.Editor.Extensions
{
    internal static class SerializedPropertyExtensions
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static T As<T>(this SerializedProperty serializedProperty)
        {
            var declaringType = serializedProperty.serializedObject.targetObject.GetType();
            var fieldInfo = declaringType.GetField(serializedProperty.propertyPath, Flags);
            return (T) fieldInfo.GetValue(serializedProperty.serializedObject.targetObject);
        }

        public static void Set<T>(this SerializedProperty serializedProperty, T value)
        {
            var declaringType = serializedProperty.serializedObject.targetObject.GetType();
            var fieldInfo = declaringType.GetField(serializedProperty.propertyPath, Flags);
            fieldInfo.SetValue(serializedProperty.serializedObject.targetObject, value);
        }
    }
}