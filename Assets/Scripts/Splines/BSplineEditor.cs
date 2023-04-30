using UnityEditor;
using UnityEngine;

namespace BezierCurve
{
   [CustomEditor(typeof(BSpline))]
   public class BSplineEditor : Editor
   {
      private BSpline refBSpline;

      private SerializedProperty timeToEvaluateCurve;

      private SerializedProperty t;

      private SerializedProperty orderK;
      
      private SerializedProperty listKnots;

      private void OnEnable()
      {
         refBSpline = (BSpline)target;
         t = serializedObject.FindProperty("t");
         orderK = serializedObject.FindProperty("orderK");
         listKnots = serializedObject.FindProperty("listKnots");
      }

      private void OnSceneGUI()
      {
         //drawzone
         for(int i = 1; i < refBSpline.listKnots.Count; i++)
            Handles.DrawLine(refBSpline.listKnots[i].knotPosition, refBSpline.listKnots[i - 1].knotPosition, 1);

         for(float tmpTime = 0; tmpTime <= 3; tmpTime += 0.2f)
         {
            var tmpPoint = refBSpline.EvaluateComulativePoint(tmpTime);
            Handles.DrawSolidDisc(tmpPoint, SceneView.lastActiveSceneView.rotation * Vector3.forward, 0.05f);
         }

         //modification zone
         EditorGUI.BeginChangeCheck();

         for(int i = 0; i < refBSpline.listKnots.Count; i++)
         {
            var tmpNewPosition = Handles.PositionHandle(refBSpline.listKnots[i].knotPosition, refBSpline.listKnots[i].knotRotation);
            var tmpNewRotation = Handles.Disc(refBSpline.listKnots[i].knotRotation, refBSpline.listKnots[i].knotPosition, Vector3.forward, 0.2f, false, 0);

            if(EditorGUI.EndChangeCheck())
            {
               Undo.RecordObject(refBSpline, "MovePosition");
               refBSpline.listKnots[i].knotPosition = tmpNewPosition;
               refBSpline.listKnots[i].knotRotation = tmpNewRotation;
            }
         }
      }

      public override void OnInspectorGUI()
      {
         serializedObject.Update();
         EditorGUILayout.PropertyField(t, new GUIContent("Time"));
         EditorGUILayout.PropertyField(orderK, new GUIContent("orderK"));
         EditorGUILayout.PropertyField(listKnots, new GUIContent("Knots"));
         serializedObject.ApplyModifiedProperties();

         if(GUILayout.Button("Add knot"))
            refBSpline.listKnots.Add(new ContainerKnot(Random.insideUnitSphere, Quaternion.identity));
      }
   }
}