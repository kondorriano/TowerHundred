using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelController))]
public class LevelControllerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Nothing")) {
            //LevelController lc = (LevelController)target;

            //lc.InstantiateVisualColliders();
        }
    }
}
