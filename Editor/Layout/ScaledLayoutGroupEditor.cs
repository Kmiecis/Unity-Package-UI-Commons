using Common.UI;
using UnityEditor;
using UnityEngine;

namespace CommonEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScaledLayoutGroup), true)]
    public class ScaledLayoutGroupEditor : Editor
    {
        private const string INDENT = "   ";

        // Defaults
        private SerializedProperty _childAlignment;
        private SerializedProperty _reverseArrangement;
        private SerializedProperty _childControlHeight;
        private SerializedProperty _childControlWidth;
        private SerializedProperty _childScaleWidth;
        private SerializedProperty _childScaleHeight;
        private SerializedProperty _childForceExpandHeight;
        private SerializedProperty _childForceExpandWidth;

        private bool _paddingFoldout = true;
        private SerializedProperty _scaledPaddingBottom;
        private SerializedProperty _scaledPaddingLeft;
        private SerializedProperty _scaledPaddingRight;
        private SerializedProperty _scaledPaddingTop;
        private SerializedProperty _scaledSpacing;

        private SerializedProperty _type;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _scaledPaddingLeft = serializedObject.FindProperty("_scaledPaddingLeft");
            _scaledPaddingRight = serializedObject.FindProperty("_scaledPaddingRight");
            _scaledPaddingTop = serializedObject.FindProperty("_scaledPaddingTop");
            _scaledPaddingBottom = serializedObject.FindProperty("_scaledPaddingBottom");
            _scaledSpacing = serializedObject.FindProperty("_scaledSpacing");
            // Defaults
            _childAlignment = serializedObject.FindProperty("m_ChildAlignment");
            _reverseArrangement = serializedObject.FindProperty("m_ReverseArrangement");
            _childControlWidth = serializedObject.FindProperty("m_ChildControlWidth");
            _childControlHeight = serializedObject.FindProperty("m_ChildControlHeight");
            _childScaleWidth = serializedObject.FindProperty("m_ChildScaleWidth");
            _childScaleHeight = serializedObject.FindProperty("m_ChildScaleHeight");
            _childForceExpandWidth = serializedObject.FindProperty("m_ChildForceExpandWidth");
            _childForceExpandHeight = serializedObject.FindProperty("m_ChildForceExpandHeight");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _paddingFoldout = EditorGUILayout.Foldout(_paddingFoldout, "Scaled Padding");
            if (_paddingFoldout)
            {
                EditorElement(_scaledPaddingLeft, INDENT + "Left");
                EditorElement(_scaledPaddingRight, INDENT + "Right");
                EditorElement(_scaledPaddingTop, INDENT + "Top");
                EditorElement(_scaledPaddingBottom, INDENT + "Bottom");
            }

            EditorElement(_scaledSpacing);

            EditorGUILayout.PropertyField(_childAlignment, true);
            EditorGUILayout.PropertyField(_reverseArrangement, true);

            EditorElementsBothHorizontal(_childControlWidth, _childControlHeight, "Control Child Size");
            EditorElementsBothHorizontal(_childScaleWidth, _childScaleHeight, "Use Child Scale");
            EditorElementsBothHorizontal(_childForceExpandWidth, _childForceExpandHeight, "Child Force Expand");

            EditorGUILayout.PropertyField(_type, true);

            serializedObject.ApplyModifiedProperties();
        }

        private void EditorElement(SerializedProperty property, string name = null)
        {
            if (name == null)
                name = property.displayName;

            if (!property.hasMultipleDifferentValues) EditorGUILayout.PropertyField(property, new GUIContent(name));
        }

        private void EditorElementsBothHorizontal(SerializedProperty p1, SerializedProperty p2, string name)
        {
            var rect = EditorGUILayout.GetControlRect();
            rect = EditorGUI.PrefixLabel(rect, -1, new GUIContent(name));
            rect.width = Mathf.Max(50.0f, (rect.width - 4.0f) / 3.0f);

            EditorGUIUtility.labelWidth = 50.0f;
            ToggleLeft(rect, p1, new GUIContent("Width"));
            rect.x += rect.width + 2.0f;
            ToggleLeft(rect, p2, new GUIContent("Height"));
            EditorGUIUtility.labelWidth = 0.0f;
        }

        private void ToggleLeft(Rect position, SerializedProperty property, GUIContent label)
        {
            var toggle = property.boolValue;
            EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
            EditorGUI.BeginChangeCheck();
            var oldIndent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            toggle = EditorGUI.ToggleLeft(position, label, toggle);
            EditorGUI.indentLevel = oldIndent;
            if (EditorGUI.EndChangeCheck())
                property.boolValue = property.hasMultipleDifferentValues ? true : !property.boolValue;
            EditorGUI.showMixedValue = false;
        }
    }
}