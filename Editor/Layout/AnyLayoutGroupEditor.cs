using Common.UI;
using UnityEditor;
using UnityEditor.UI;

namespace CommonEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AnyLayoutGroup))]
    public class AnyLayoutGroupEditor : HorizontalOrVerticalLayoutGroupEditor
    {
        private SerializedProperty _type;

        protected override void OnEnable()
        {
            base.OnEnable();

            _type = serializedObject.FindProperty("_type");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_type, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}