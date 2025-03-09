using Common.UI;
using UnityEditor;
using UnityEditor.UI;

namespace CommonEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScaledText))]
    public class ScaledTextEditor : TextEditor
    {
        private SerializedProperty _fontScale;

        protected override void OnEnable()
        {
            base.OnEnable();
            _fontScale = serializedObject.FindProperty("_fontScale");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(_fontScale);

            serializedObject.ApplyModifiedProperties();
        }
    }
}