using Common.UI;
using UnityEditor;
using UnityEngine;

namespace Gui
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AspectRatioFitterElement), true)]
    public class AspectRatioFitterElementEditor : Editor
    {
        private SerializedProperty m_IgnoreLayout;
        private SerializedProperty m_FlexibleWidth;
        private SerializedProperty m_FlexibleHeight;

        private SerializedProperty _useMinWidth;
        private SerializedProperty _useMinHeight;
        private SerializedProperty _usePreferredWidth;
        private SerializedProperty _usePreferredHeight;

        private void OnEnable()
        {
            m_IgnoreLayout = serializedObject.FindProperty(nameof(m_IgnoreLayout));
            m_FlexibleWidth = serializedObject.FindProperty(nameof(m_FlexibleWidth));
            m_FlexibleHeight = serializedObject.FindProperty(nameof(m_FlexibleHeight));

            _useMinWidth = serializedObject.FindProperty(nameof(_useMinWidth));
            _useMinHeight = serializedObject.FindProperty(nameof(_useMinHeight));
            _usePreferredWidth = serializedObject.FindProperty(nameof(_usePreferredWidth));
            _usePreferredHeight = serializedObject.FindProperty(nameof(_usePreferredHeight));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_IgnoreLayout);

            if (!m_IgnoreLayout.boolValue)
            {
                var script = (AspectRatioFitterElement)target;

                LayoutElementShow(_useMinWidth, script.minWidth);
                LayoutElementShow(_useMinHeight, script.minHeight);
                LayoutElementShow(_usePreferredWidth, script.preferredWidth);
                LayoutElementShow(_usePreferredHeight, script.preferredHeight);

                LayoutElementField(m_FlexibleWidth, 1);
                LayoutElementField(m_FlexibleHeight, 1);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void LayoutElementShow(SerializedProperty property, float propertyValue)
        {
            var position = EditorGUILayout.GetControlRect();

            var label = EditorGUI.BeginProperty(position, null, property);

            var fieldPosition = EditorGUI.PrefixLabel(position, label);

            var toggleRect = fieldPosition;
            toggleRect.width = 16;

            var floatFieldRect = fieldPosition;
            floatFieldRect.xMin += 16;

            property.boolValue = EditorGUI.ToggleLeft(toggleRect, GUIContent.none, property.boolValue);

            if (!property.hasMultipleDifferentValues && property.boolValue)
            {
                EditorGUIUtility.labelWidth = 4;
                GUI.enabled = false;
                EditorGUI.FloatField(floatFieldRect, new GUIContent(" "), propertyValue);
                GUI.enabled = true;
                EditorGUIUtility.labelWidth = 0;
            }

            EditorGUI.EndProperty();
        }

        private void LayoutElementField(SerializedProperty property, float defaultValue)
        {
            var position = EditorGUILayout.GetControlRect();

            var label = EditorGUI.BeginProperty(position, null, property);

            var fieldPosition = EditorGUI.PrefixLabel(position, label);

            var toggleRect = fieldPosition;
            toggleRect.width = 16;

            var floatFieldRect = fieldPosition;
            floatFieldRect.xMin += 16;

            EditorGUI.BeginChangeCheck();
            bool enabled = EditorGUI.ToggleLeft(toggleRect, GUIContent.none, property.floatValue >= 0);
            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = (enabled ? defaultValue : -1);
            }

            if (!property.hasMultipleDifferentValues && property.floatValue >= 0)
            {
                EditorGUIUtility.labelWidth = 4; // Small invisible label area for drag zone functionality

                EditorGUI.BeginChangeCheck();
                float newValue = EditorGUI.FloatField(floatFieldRect, new GUIContent(" "), property.floatValue);
                if (EditorGUI.EndChangeCheck())
                {
                    property.floatValue = Mathf.Max(0, newValue);
                }

                EditorGUIUtility.labelWidth = 0;
            }

            EditorGUI.EndProperty();
        }
    }
}