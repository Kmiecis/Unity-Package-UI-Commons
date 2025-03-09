﻿using Common.UI;
using UnityEditor;
using UnityEngine;

namespace CommonEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScaledLayoutElement), true)]
    public class ScaledLayoutElementEditor : Editor
    {
        private SerializedProperty _ignoreLayout;
        private SerializedProperty _flexibleHeight;
        private SerializedProperty _flexibleWidth;
        private SerializedProperty _layoutPriority;

        // Defaults
        private SerializedProperty _minHeightScale;
        private SerializedProperty _minWidthScale;
        private SerializedProperty _preferredHeightScale;
        private SerializedProperty _preferredWidthScale;

        protected virtual void OnEnable()
        {
            _minWidthScale = serializedObject.FindProperty("_minWidthScale");
            _minHeightScale = serializedObject.FindProperty("_minHeightScale");
            _preferredWidthScale = serializedObject.FindProperty("_preferredWidthScale");
            _preferredHeightScale = serializedObject.FindProperty("_preferredHeightScale");
            // Defaults
            _ignoreLayout = serializedObject.FindProperty("m_IgnoreLayout");
            _flexibleWidth = serializedObject.FindProperty("m_FlexibleWidth");
            _flexibleHeight = serializedObject.FindProperty("m_FlexibleHeight");
            _layoutPriority = serializedObject.FindProperty("m_LayoutPriority");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_ignoreLayout);

            if (!_ignoreLayout.boolValue)
            {
                EditorGUILayout.Space();

                LayoutElementField(_minWidthScale, 0.0f);
                LayoutElementField(_minHeightScale, 0.0f);
                LayoutElementField(_preferredWidthScale, 0.0f);
                LayoutElementField(_preferredHeightScale, 0.0f);
                LayoutElementField(_flexibleWidth, 1.0f);
                LayoutElementField(_flexibleHeight, 1.0f);
            }

            EditorGUILayout.PropertyField(_layoutPriority);

            serializedObject.ApplyModifiedProperties();
        }

        private void LayoutElementField(SerializedProperty property, float defaultValue)
        {
            var position = EditorGUILayout.GetControlRect();

            var label = EditorGUI.BeginProperty(position, null, property);

            var fieldPosition = EditorGUI.PrefixLabel(position, label);

            var toggleRect = fieldPosition;
            toggleRect.width = 16.0f;

            var floatFieldRect = fieldPosition;
            floatFieldRect.xMin += 16.0f;

            EditorGUI.BeginChangeCheck();
            var enabled = EditorGUI.ToggleLeft(toggleRect, GUIContent.none, property.floatValue >= 0.0f);
            if (EditorGUI.EndChangeCheck()) property.floatValue = enabled ? defaultValue : -1.0f;

            if (!property.hasMultipleDifferentValues && property.floatValue >= 0.0f)
            {
                EditorGUIUtility.labelWidth = 4.0f; // Small invisible label area for drag zone functionality
                EditorGUI.BeginChangeCheck();
                var newValue = EditorGUI.FloatField(floatFieldRect, new GUIContent(" "), property.floatValue);
                if (EditorGUI.EndChangeCheck()) property.floatValue = Mathf.Max(0, newValue);
                EditorGUIUtility.labelWidth = 0.0f;
            }

            EditorGUI.EndProperty();
        }
    }
}