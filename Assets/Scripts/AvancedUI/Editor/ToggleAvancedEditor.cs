#if UNITY_EDITOR
using UnityEditor;


namespace AvancedUI.Editor
{
   [CanEditMultipleObjects]
   [CustomEditor(typeof(ToggleAvanced))]
   public class ToggleAvancedEditor : UnityEditor.UI.ToggleEditor
   {
      private SerializedProperty OnTogglePointerDown;

      private SerializedProperty ToggleOn;

      private SerializedProperty ToggleOff;
      
      private SerializedProperty ToggleOnSiblingIndex;

      protected override void OnEnable()
      {
         base.OnEnable();
         OnTogglePointerDown = serializedObject.FindProperty("OnTogglePointerDown");
         ToggleOn = serializedObject.FindProperty("ToggleOn");
         ToggleOff = serializedObject.FindProperty("ToggleOff");
         ToggleOnSiblingIndex = serializedObject.FindProperty("ToggleOnSiblingIndex");
      }

      public override void OnInspectorGUI()
      {
         base.OnInspectorGUI();
         serializedObject.Update();
         EditorGUILayout.Space();
         EditorGUILayout.PropertyField(OnTogglePointerDown);
         EditorGUILayout.PropertyField(ToggleOn);
         EditorGUILayout.PropertyField(ToggleOff);
         EditorGUILayout.PropertyField(ToggleOnSiblingIndex);
         serializedObject.ApplyModifiedProperties();
      }
   }
}
#endif