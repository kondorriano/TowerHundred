using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CollisionMap))]
public class CollisionMapInspector : Editor {

    Texture2D[] guiTextures;

    void OnEnable() {
        guiTextures = new Texture2D[(int)CollisionMap.CollisionTile.Length];

        string path = "GUItextures\\";

        for (int i = 0; i < guiTextures.Length; ++i) {
            guiTextures[i] = (Texture2D) Resources.Load(path + ((CollisionMap.CollisionTile)i).ToString());
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CollisionMap map = (CollisionMap)target;

        if (map.columns == null) {
            EditorGUILayout.LabelField("Map not initialized", EditorStyles.boldLabel);
        }

        if (map.width == 0 || map.height == 0) return;


        EditorGUILayout.LabelField("Width: "+map.width + " Height: "+map.height, EditorStyles.boldLabel);

        Rect lastRect = GUILayoutUtility.GetLastRect();

        int width = Screen.width;
        int height = Screen.height - (int)lastRect.position.y;

        float w = width;
        float h = w * map.height / map.width;
        if (h > height) {
            h = height;
            w = h * map.width / map.height;
        }

        float deltaPixels = Mathf.Min(16f, w / map.width);
        w = deltaPixels * map.width;
        h = deltaPixels * map.height;

        GUIStyle style = new GUIStyle();

        style.fixedWidth = Screen.width;
        Rect pos = new Rect();
        pos.width = deltaPixels;
        pos.height = deltaPixels;

        float baseX = (width - w) / 2f;
        pos.x = baseX;
        float basey = lastRect.position.y + lastRect.height + 2 + (map.height - 1) * deltaPixels;
        pos.y = basey;
        
        EditorGUILayout.BeginVertical();
        for (int j = map.height - 1; j >= 0; --j) {
            for (int i = 0; i < map.width; ++i) {
                pos.x = baseX + i * deltaPixels;
                pos.y = basey - j * deltaPixels;
                GUI.DrawTexture(pos, guiTextures[(int)map.columns[i].rows[j]]);
            }
        }
        EditorGUILayout.EndVertical();
    }
}
