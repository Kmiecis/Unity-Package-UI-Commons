using Common.UI;
using UnityEditor;
using UnityEditor.UI;

namespace CommonEditor.UI
{
    [CustomEditor(typeof(ClearGraphic))]
    public class ClearGraphicEditor : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}